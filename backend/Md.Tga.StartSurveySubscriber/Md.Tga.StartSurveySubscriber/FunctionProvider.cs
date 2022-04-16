namespace Md.Tga.StartSurveySubscriber
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.Common.Database;
    using Md.GoogleCloudFunctions.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;
    using Surveys.Common.PubSub.Contracts.Logic;
    using IPerson = Md.Tga.Common.Contracts.Models.IPerson;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IStartSurveyMessage, Function>
    {
        private readonly IGameReadOnlyDatabase gameReadOnlyDatabase;

        private readonly IPlayerMappingsReadOnlyDatabase playerMappingsReadOnlyDatabase;

        /// <summary>
        ///     Access pub/sub client for saving surveys.
        /// </summary>
        private readonly ISaveSurveyPubSubClient pubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="pubSubClient">Access pub/sub client for saving surveys.</param>
        /// <param name="gameReadOnlyDatabase">Read the games of the game series.</param>
        /// <param name="playerMappingsReadOnlyDatabase">Read the mappings of the game.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISaveSurveyPubSubClient pubSubClient,
            IGameReadOnlyDatabase gameReadOnlyDatabase,
            IPlayerMappingsReadOnlyDatabase playerMappingsReadOnlyDatabase
        )
            : base(logger)
        {
            this.pubSubClient = pubSubClient;
            this.gameReadOnlyDatabase = gameReadOnlyDatabase;
            this.playerMappingsReadOnlyDatabase = playerMappingsReadOnlyDatabase;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IStartSurveyMessage message)
        {
            var survey = await this.CreateSurvey(message);
            var saveSurveyMessage = new SaveSurveyMessage(message.ProcessId, survey);
            await this.pubSubClient.PublishAsync(saveSurveyMessage);
        }

        /// <summary>
        ///     Create the choices of the survey questions.
        /// </summary>
        /// <param name="countries">The countries of the game series.</param>
        /// <returns>An <see cref="IEnumerable{T}" /> of <see cref="IChoice" />.</returns>
        private static IEnumerable<IChoice> CreateChoices(IEnumerable<ICountry> countries)
        {
            var order = 0;
            foreach (var country in countries)
            {
                yield return new Choice(
                    country.Id,
                    country.Name,
                    true,
                    order++);
            }
        }

        /// <summary>
        ///     Create an info for the survey.
        /// </summary>
        /// <param name="message">The incoming pub/sub message.</param>
        /// <returns>An info <see cref="string" />.</returns>
        private static async Task<string> CreateInfo(IStartSurveyMessage message)
        {
            await Task.CompletedTask;
            return "info";
        }

        /// <summary>
        ///     Map <see cref="Surveys.Common.Models.Person" /> to <see cref="Participant" />.
        /// </summary>
        /// <param name="players">The players of the game series.</param>
        /// <param name="questions">The questions of the survey.</param>
        /// <param name="statistics">A player-country-statistic.</param>
        /// <returns>An <see cref="IEnumerable{T}" /> of <see cref="Participant" />.</returns>
        private static IEnumerable<IParticipant> CreateParticipants(
            IEnumerable<IPerson> players,
            IList<IQuestion> questions,
            IDictionary<string, IDictionary<string, int>> statistics
        )
        {
            var order = 0;
            foreach (var player in players)
            {
                var stats = statistics[player.Id].OrderBy(kvp => kvp.Value).ToArray();
                var questionReferences = questions.Select(
                        (question, i) => new QuestionReference(
                            question.Id,
                            question.Choices.First(choice => choice.Id == stats[i].Key).Id))
                    .ToArray();
                yield return new Participant(
                    player.Id,
                    player.Email,
                    player.Name,
                    questionReferences,
                    order++);
            }
        }

        /// <summary>
        ///     Create the questions of a survey.
        /// </summary>
        /// <param name="gameSeries">The game series to be processed.</param>
        /// <returns>An <see cref="IEnumerable{T}" /> of <see cref="IQuestion" />.</returns>
        private static IEnumerable<IQuestion> CreateQuestions(IGameSeries gameSeries)
        {
            var questions = new[] {Translations.Question1, Translations.Question2, Translations.Question3};

            return questions.Select(
                (text, order) => new Question(
                    Guid.NewGuid().ToString(),
                    text,
                    FunctionProvider.CreateChoices(gameSeries.Countries).ToArray(),
                    order));
        }

        /// <summary>
        ///     Create a new survey.
        /// </summary>
        /// <param name="message">The incoming pub/sub message.</param>
        /// <returns>An <see cref="ISurvey" />.</returns>
        private async Task<ISurvey> CreateSurvey(IStartSurveyMessage message)
        {
            var statistic = await this.ReadStatistic(message.GameSeries);
            var questions = FunctionProvider.CreateQuestions(message.GameSeries).ToArray();
            var participants = FunctionProvider.CreateParticipants(message.GameSeries.Players, questions, statistic)
                .ToArray();
            var info = await FunctionProvider.CreateInfo(message);
            return new Survey(
                null,
                null,
                message.Game.DocumentId,
                message.Game.Name,
                info,
                "link",
                new Person(
                    message.GameSeries.Organizer.Id,
                    message.GameSeries.Organizer.Email,
                    message.GameSeries.Organizer.Name),
                participants,
                questions);
        }

        /// <summary>
        ///     Read the history of the game series.
        /// </summary>
        /// <param name="gameSeries">The game series for that a static is generated.</param>
        /// <returns>The history of the game series.</returns>
        private async Task<IDictionary<string, IDictionary<string, int>>> ReadStatistic(IGameSeries gameSeries)
        {
            var statistic = new Dictionary<string, IDictionary<string, int>>();
            foreach (var gameSeriesPlayer in gameSeries.Players)
            {
                statistic[gameSeriesPlayer.Id] = new Dictionary<string, int>();
                foreach (var gameSeriesCountry in gameSeries.Countries)
                {
                    statistic[gameSeriesPlayer.Id][gameSeriesCountry.Id] = 0;
                }
            }

            // read all games
            var games = (await this.gameReadOnlyDatabase.ReadManyAsync(
                DatabaseObject.ParentDocumentIdName,
                gameSeries.DocumentId)).ToArray();

            if (!games.Any())
            {
                return statistic;
            }

            // read all player-country-mappings
            var tasks = games.Select(
                game => this.playerMappingsReadOnlyDatabase.ReadOneAsync(
                    DatabaseObject.ParentDocumentIdName,
                    game.DocumentId));

            foreach (var task in tasks)
            {
                var mapping = await task;
                if (mapping == null)
                {
                    continue;
                }

                foreach (var countryMapping in mapping.PlayerCountryMappings)
                {
                    statistic[countryMapping.PlayerId][countryMapping.CountryId] += 1;
                }
            }

            return statistic;
        }
    }
}
