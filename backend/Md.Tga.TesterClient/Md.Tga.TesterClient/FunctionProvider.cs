namespace Md.Tga.TesterClient
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Md.Common.Database;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : IFunctionProvider
    {
        private readonly IGameReadOnlyDatabase gameReadOnlyDatabase;

        private readonly IGameSeriesReadOnlyDatabase gameSeriesReadOnlyDatabase;

        /// <summary>
        ///     Client for sending a message to pub/sub.
        /// </summary>
        private readonly IStartGameSeriesPubSubClient pubSubClient;

        private readonly ISaveSurveyResultPubSubClient saveSurveyResultPubSubClient;

        private readonly ISurveyReadOnlyDatabase surveyReadOnlyDatabase;

        /// <summary>
        ///     Access test data.
        /// </summary>
        private readonly ITestDataReadOnlyDatabase testDataReadOnlyDatabase;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="pubSubClient">Client for sending a message to pub/sub.</param>
        /// <param name="testDataReadOnlyDatabase">Access test data.</param>
        public FunctionProvider(
            IStartGameSeriesPubSubClient pubSubClient,
            ITestDataReadOnlyDatabase testDataReadOnlyDatabase,
            IGameSeriesReadOnlyDatabase gameSeriesReadOnlyDatabase,
            IGameReadOnlyDatabase gameReadOnlyDatabase,
            ISurveyReadOnlyDatabase surveyReadOnlyDatabase,
            ISaveSurveyResultPubSubClient saveSurveyResultPubSubClient
        )

        {
            this.saveSurveyResultPubSubClient = saveSurveyResultPubSubClient;
            this.pubSubClient = pubSubClient ?? throw new ArgumentNullException(nameof(pubSubClient));
            this.testDataReadOnlyDatabase = testDataReadOnlyDatabase ??
                                            throw new ArgumentNullException(nameof(testDataReadOnlyDatabase));
            this.gameSeriesReadOnlyDatabase = gameSeriesReadOnlyDatabase;
            this.gameReadOnlyDatabase = gameReadOnlyDatabase;
            this.surveyReadOnlyDatabase = surveyReadOnlyDatabase;
        }

        /// <summary>
        ///     Read the test case from the database and publish the message to pub/sub to start the process.
        /// </summary>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task InitializeGameSeries()
        {
            var externalId = Guid.NewGuid().ToString();
            var gameSeries = await this.InitializeGameSeries(externalId);
            var game = await this.ReadGame(gameSeries);
            var survey = await this.ReadSurvey(game);
            await this.SubmitSurveyResults(survey);
        }

        private async Task<IGameSeries> InitializeGameSeries(string externalId)
        {
            var startGameSeriesMessage = await this.testDataReadOnlyDatabase.ReadStartGameSeriesMessageAsync();

            var message = new StartGameSeriesMessage(
                Guid.NewGuid().ToString(),
                new StartGameSeries(
                    externalId,
                    Guid.NewGuid().ToString(),
                    startGameSeriesMessage.GameSeries.GameType,
                    startGameSeriesMessage.GameSeries.Organizer,
                    startGameSeriesMessage.GameSeries.Players));
            await this.pubSubClient.PublishAsync(message);

            for (var i = 1; i < 10; ++i)
            {
                var gameSeries = await this.gameSeriesReadOnlyDatabase.ReadOneAsync(
                    GameSeries.ExternalIdName,
                    externalId);
                if (gameSeries == null)
                {
                    Thread.Sleep(i * 1000);
                }
                else
                {
                    return gameSeries;
                }
            }

            throw new Exception($"Game series not found: {externalId}");
        }

        private async Task<IGame> ReadGame(IGameSeries gameSeries)
        {
            for (var i = 0; i < 10; i++)
            {
                var game = await this.gameReadOnlyDatabase.ReadOneAsync(
                    DatabaseObject.ParentDocumentIdName,
                    gameSeries.DocumentId);
                if (game == null)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    return game;
                }
            }

            throw new Exception($"Cannot read game for game series {gameSeries.DocumentId}");
        }

        private async Task<ISurvey> ReadSurvey(IGame game)
        {
            for (var i = 0; i < 10; i++)
            {
                var survey = await this.surveyReadOnlyDatabase.ReadOneAsync(
                    DatabaseObject.ParentDocumentIdName,
                    game.DocumentId);
                if (survey == null)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    return survey;
                }
            }

            throw new Exception($"Cannot read survey for game {game.DocumentId}");
        }

        private async Task SubmitSurveyResults(ISurvey survey)
        {
            foreach (var surveyParticipant in survey.Participants)
            {
                await this.saveSurveyResultPubSubClient.PublishAsync(
                    new SaveSurveyResultMessage(
                        Guid.NewGuid().ToString(),
                        new SurveyResult(
                            null,
                            null,
                            survey.DocumentId,
                            surveyParticipant.Id,
                            false,
                            survey.Questions.Select(q => new QuestionReference(q.Id, q.Choices.First().Id))
                                .ToArray())));
            }
        }
    }
}
