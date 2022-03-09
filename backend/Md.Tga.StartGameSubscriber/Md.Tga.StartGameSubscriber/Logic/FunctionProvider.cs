namespace Md.Tga.StartGameSubscriber.Logic
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Md.Tga.StartGameSubscriber.Contracts;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;
    using Person = Surveys.Common.Models.Person;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IStartGameMessage, Function>
    {
        /// <summary>
        ///     Access to the database collection games.
        /// </summary>
        private readonly IReadOnlyDatabase gamesDatabase;

        /// <summary>
        ///     Access to the database collection game-series.
        /// </summary>
        private readonly IReadOnlyDatabase gameSeriesDatabase;

        /// <summary>
        ///     Access pub/sub.
        /// </summary>
        private readonly IPubSubClient pubSubClient;

        /// <summary>
        ///     Access to the database collection translations.
        /// </summary>
        private readonly ITranslationsReadOnlyDatabase translationsDatabase;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gameSeriesDatabase">Access to the database collection game-series.</param>
        /// <param name="gamesDatabase">Access to the database collection games.</param>
        /// <param name="translationsDatabase">Access to the database collection translations.</param>
        /// <param name="pubSubClient">Client for accessing pub/sub.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameSeriesReadOnlyDatabase gameSeriesDatabase,
            IGamesReadOnlyDatabase gamesDatabase,
            ITranslationsReadOnlyDatabase translationsDatabase,
            IPubSubClient pubSubClient
        )
            : base(logger)
        {
            this.gameSeriesDatabase = gameSeriesDatabase ?? throw new ArgumentNullException(nameof(gameSeriesDatabase));
            this.gamesDatabase = gamesDatabase ?? throw new ArgumentNullException(nameof(gamesDatabase));
            this.translationsDatabase =
                translationsDatabase ?? throw new ArgumentNullException(nameof(translationsDatabase));
            this.pubSubClient = pubSubClient ?? throw new ArgumentNullException(nameof(pubSubClient));
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IStartGameMessage message)
        {
            var gameSeries = await this.ReadGameSeries(message);

            var surveyName = string.Empty;
            var surveyInfo = string.Empty;
            var surveyInfoLink = string.Empty;
            var answerDefault = string.Empty;
            var questions = new[]
            {
                string.Empty,
                string.Empty,
                string.Empty
            };

            var translations = await this.translationsDatabase.ReadGermanTranslations();
            if (translations != null)
            {
                if (translations.ContainsKey("game-name"))
                {
                    surveyName = translations["game-name"];
                }

                if (translations.ContainsKey("survey-info"))
                {
                    surveyInfo = translations["survey-info"];
                }

                if (translations.ContainsKey("survey-info-link"))
                {
                    surveyInfoLink = translations["survey-info-link"];
                }

                if (translations.ContainsKey("answer-default"))
                {
                    answerDefault = translations["answer-default"];
                }

                if (translations.ContainsKey("survey-question-0"))
                {
                    questions[0] = translations["survey-question-0"];
                }

                if (translations.ContainsKey("survey-question-1"))
                {
                    questions[1] = translations["survey-question-1"];
                }

                if (translations.ContainsKey("survey-question-2"))
                {
                    questions[2] = translations["survey-question-2"];
                }
            }

            var initializeSurveyMessage = this.BuildInitializeSurveyMessage(
                message.ProcessId,
                gameSeries,
                surveyName,
                surveyInfo,
                surveyInfoLink,
                answerDefault,
                questions);
            await this.pubSubClient.PublishAsync(initializeSurveyMessage);
        }

        private IInitializeSurveyMessage BuildInitializeSurveyMessage(
            string processId,
            IGameSeries gameSeries,
            string surveyName,
            string surveyInfo,
            string surveyInfoLink,
            string answerDefault,
            string[] questionTexts
        )
        {
            var choices = gameSeries.Countries.Select(
                (country, i) => new Choice(
                    Guid.NewGuid().ToString(),
                    country.Name,
                    true,
                    i + 1));
            choices = choices.Prepend(
                new Choice(
                    Guid.NewGuid().ToString(),
                    answerDefault,
                    false,
                    0)).ToArray();

            var questions = new[]
            {
                new Question(
                    Guid.NewGuid().ToString(),
                    questionTexts[0],
                    choices,
                    0),
                new Question(
                    Guid.NewGuid().ToString(),
                    questionTexts[1],
                    choices,
                    0),
                new Question(
                    Guid.NewGuid().ToString(),
                    questionTexts[2],
                    choices,
                    0)
            };

            var participants = gameSeries.Players.Select(
                (player, i) => new Participant(
                    player.Id,
                    player.Email,
                    player.Name,
                    questions.Select(
                        question => new QuestionReference(
                            question.Id,
                            question.Choices.First(choice => choice.Order == 0).Id)).ToArray(),
                    i));

            return new InitializeSurveyMessage(
                new Survey(
                    Guid.NewGuid().ToString(),
                    surveyName,
                    surveyInfo,
                    surveyInfoLink,
                    new Person(gameSeries.Organizer.Id, gameSeries.Organizer.Email, gameSeries.Organizer.Name),
                    participants,
                    questions),
                processId);
        }

        /// <summary>
        ///     Returns the <see cref="IStartGameMessage.GameSeries" /> or reads the data from the database using
        ///     <see cref="IStartGameMessage.InternalId" />.
        /// </summary>
        /// <param name="message">The incoming message of the pub/sub function.</param>
        /// <returns>The game series for that a new game will be started.</returns>
        private async Task<IGameSeries> ReadGameSeries(IStartGameMessage message)
        {
            if (message.GameSeries != null)
            {
                return message.GameSeries;
            }

            var dictionary = await this.gameSeriesDatabase.ReadByDocumentIdAsync(message.InternalId);
            if (dictionary == null)
            {
                throw new InvalidOperationException($"Unknown game series internal id: {message.InternalId}");
            }

            return GameSeries.FromDictionary(dictionary);
        }
    }
}
