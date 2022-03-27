namespace Md.Tga.StartGameSubscriber.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
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
        ///     The application configuration.
        /// </summary>
        private readonly IFunctionConfiguration configuration;

        /// <summary>
        ///     Access to the database collection game-series.
        /// </summary>
        private readonly IGameSeriesReadOnlyDatabase gameSeriesDatabase;

        /// <summary>
        ///     Access pub/sub.
        /// </summary>
        private readonly IPubSubClient initializeSurveyPubSubClient;

        /// <summary>
        ///     Access pub/sub.
        /// </summary>
        private readonly ISaveGamePubSubClient saveGamePubSubClient;

        /// <summary>
        ///     Access to the database collection translations.
        /// </summary>
        private readonly ITranslationsReadOnlyDatabase translationsDatabase;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gameSeriesDatabase">Access to the database collection game-series.</param>
        /// <param name="translationsDatabase">Access to the database collection translations.</param>
        /// <param name="initializeSurveyPubSubClient">Client for accessing pub/sub.</param>
        /// <param name="saveGamePubSubClient">Client for accessing pub/sub.</param>
        /// <param name="configuration">The application configuration.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameSeriesReadOnlyDatabase gameSeriesDatabase,
            ITranslationsReadOnlyDatabase translationsDatabase,
            IInitializeSurveyPubSubClient initializeSurveyPubSubClient,
            ISaveGamePubSubClient saveGamePubSubClient,
            IFunctionConfiguration configuration
        )
            : base(logger)
        {
            this.gameSeriesDatabase = gameSeriesDatabase ?? throw new ArgumentNullException(nameof(gameSeriesDatabase));
            this.translationsDatabase =
                translationsDatabase ?? throw new ArgumentNullException(nameof(translationsDatabase));
            this.initializeSurveyPubSubClient = initializeSurveyPubSubClient ??
                                                throw new ArgumentNullException(nameof(initializeSurveyPubSubClient));
            this.saveGamePubSubClient =
                saveGamePubSubClient ?? throw new ArgumentNullException(nameof(saveGamePubSubClient));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IStartGameMessage message)
        {
            var gameSeries = await this.ReadGameSeries(message);

            var translations =
                await this.translationsDatabase.ReadByDocumentIdAsync(this.configuration.TranslationsDocument);
            if (translations == null)
            {
                throw new ArgumentException($"Cannot read translations fir {this.configuration.TranslationsDocument}");
            }

            var newGameSurvey = translations.German.NewGameSurvey;
            var initializeSurveyMessage = FunctionProvider.BuildInitializeSurveyMessage(
                message.ProcessId,
                gameSeries,
                newGameSurvey.GameName,
                newGameSurvey.SurveyInfo,
                newGameSurvey.SurveyInfoLink,
                newGameSurvey.AnswerDefault,
                newGameSurvey.Questions.ToArray());

            var saveGameMessage = FunctionProvider.BuildSaveGameMessage(
                message.InternalGameSeriesId,
                initializeSurveyMessage);
            await this.saveGamePubSubClient.PublishAsync(saveGameMessage);

            await this.initializeSurveyPubSubClient.PublishAsync(initializeSurveyMessage);
        }

        private static IInitializeSurveyMessage BuildInitializeSurveyMessage(
            string processId,
            IGameSeries gameSeries,
            string surveyName,
            string surveyInfo,
            string surveyInfoLink,
            string answerDefault,
            IReadOnlyList<string> questionTexts
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
                        0))
                .ToArray();

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
                                question.Choices.First(choice => choice.Order == 0).Id))
                        .ToArray(),
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

        private static ISaveGameMessage BuildSaveGameMessage(
            string internalGameSeriesId,
            IInitializeSurveyMessage message
        )
        {
            return new SaveGameMessage(
                message.ProcessId,
                new Game(
                    Guid.NewGuid().ToString(),
                    message.Survey.Name,
                    internalGameSeriesId,
                    message.Survey.Id));
        }

        /// <summary>
        ///     Reads the data from the database using
        ///     <see cref="IStartGameMessage.InternalGameSeriesId" />.
        /// </summary>
        /// <param name="message">The incoming message of the pub/sub function.</param>
        /// <returns>The game series for that a new game will be started.</returns>
        private async Task<IGameSeries> ReadGameSeries(IStartGameMessage message)
        {
            var gameSeries = await this.gameSeriesDatabase.ReadByDocumentIdAsync(message.InternalGameSeriesId);
            if (gameSeries == null)
            {
                throw new InvalidOperationException($"Unknown game series internal id: {message.InternalGameSeriesId}");
            }

            return gameSeries;
        }
    }
}
