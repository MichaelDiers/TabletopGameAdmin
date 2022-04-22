namespace Md.Tga.TesterClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Md.Common.Database;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;
    using Surveys.Common.PubSub.Contracts.Logic;
    using Status = Md.Tga.Common.Contracts.Models.Status;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : IFunctionProvider
    {
        /// <summary>
        ///     Max iterations for waiting on results.
        /// </summary>
        private const int MaxIterations = 20;

        /// <summary>
        ///     The sleep value in ms.
        /// </summary>
        private const int Sleep = 1000;

        /// <summary>
        ///     Access the game database collection.
        /// </summary>
        private readonly IGameReadOnlyDatabase gameReadOnlyDatabase;

        /// <summary>
        ///     Access the game series database collection.
        /// </summary>
        private readonly IGameSeriesReadOnlyDatabase gameSeriesReadOnlyDatabase;

        /// <summary>
        ///     Access the game status database collection.
        /// </summary>
        private readonly IGameStatusReadOnlyDatabase gameStatusReadOnlyDatabase;

        /// <summary>
        ///     Log messages to the cloud.
        /// </summary>
        private readonly ILogger<Function> logger;

        /// <summary>
        ///     Access the player mappings database collection.
        /// </summary>
        private readonly IPlayerMappingsReadOnlyDatabase playerMappingsReadOnlyDatabase;

        /// <summary>
        ///     Send messages for starting a new game series.
        /// </summary>
        private readonly IStartGameSeriesPubSubClient pubSubClient;

        /// <summary>
        ///     Send messages for saving a survey.
        /// </summary>
        private readonly ISaveSurveyResultPubSubClient saveSurveyResultPubSubClient;

        /// <summary>
        ///     Send messages for starting the game termination process.
        /// </summary>
        private readonly IStartGameTerminationPubSubClient startGameTerminationPubSubClient;

        /// <summary>
        ///     Access the survey database collection.
        /// </summary>
        private readonly ISurveyReadOnlyDatabase surveyReadOnlyDatabase;

        /// <summary>
        ///     Access the survey status database collection.
        /// </summary>
        private readonly ISurveyStatusReadOnlyDatabase surveyStatusReadOnlyDatabase;

        /// <summary>
        ///     Access the test data database collection.
        /// </summary>
        private readonly ITestDataReadOnlyDatabase testDataReadOnlyDatabase;

        /// <summary>
        ///     Initializes a new instance of the FunctionProvider class.
        /// </summary>
        /// <param name="logger">Log messages to the cloud.</param>
        /// <param name="startGameSeriesPubSubClient">Send a message for starting a new game series.</param>
        /// <param name="testDataReadOnlyDatabase">Access the test data database collection.</param>
        /// <param name="gameSeriesReadOnlyDatabase">Access the game series database collection.</param>
        /// <param name="gameReadOnlyDatabase">Access the game database collection.</param>
        /// <param name="surveyReadOnlyDatabase">Access the survey database collection.</param>
        /// <param name="saveSurveyResultPubSubClient">Send a message for saving a survey.</param>
        /// <param name="playerMappingsReadOnlyDatabase">Access the player mappings database collection.</param>
        /// <param name="startGameTerminationPubSubClient">Send a message for starting the game termination process.</param>
        /// <param name="surveyStatusReadOnlyDatabase">Access the survey status database collection.</param>
        /// <param name="gameStatusReadOnlyDatabase">Access the game status database collection.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IStartGameSeriesPubSubClient startGameSeriesPubSubClient,
            ITestDataReadOnlyDatabase testDataReadOnlyDatabase,
            IGameSeriesReadOnlyDatabase gameSeriesReadOnlyDatabase,
            IGameReadOnlyDatabase gameReadOnlyDatabase,
            ISurveyReadOnlyDatabase surveyReadOnlyDatabase,
            ISaveSurveyResultPubSubClient saveSurveyResultPubSubClient,
            IPlayerMappingsReadOnlyDatabase playerMappingsReadOnlyDatabase,
            IStartGameTerminationPubSubClient startGameTerminationPubSubClient,
            ISurveyStatusReadOnlyDatabase surveyStatusReadOnlyDatabase,
            IGameStatusReadOnlyDatabase gameStatusReadOnlyDatabase
        )

        {
            this.logger = logger;
            this.saveSurveyResultPubSubClient = saveSurveyResultPubSubClient;
            this.playerMappingsReadOnlyDatabase = playerMappingsReadOnlyDatabase;
            this.startGameTerminationPubSubClient = startGameTerminationPubSubClient;
            this.surveyStatusReadOnlyDatabase = surveyStatusReadOnlyDatabase;
            this.gameStatusReadOnlyDatabase = gameStatusReadOnlyDatabase;
            this.pubSubClient = startGameSeriesPubSubClient;
            this.testDataReadOnlyDatabase = testDataReadOnlyDatabase;
            this.gameSeriesReadOnlyDatabase = gameSeriesReadOnlyDatabase;
            this.gameReadOnlyDatabase = gameReadOnlyDatabase;
            this.surveyReadOnlyDatabase = surveyReadOnlyDatabase;
        }

        /// <summary>
        ///     Read the test case from the database and publish the message to pub/sub to start the process.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result is a json formatted string.</returns>
        public async Task<string> InitializeGameSeries()
        {
            var jsonArray = new JArray();
            try
            {
                var externalId = Guid.NewGuid().ToString();
                this.logger.LogInformation($"Using external id {externalId}");
                jsonArray.Add($"Using external id {externalId}");

                var gameSeries = await this.InitializeGameSeries(externalId);
                this.logger.LogInformation($"GameSeries: {gameSeries.DocumentId}");
                jsonArray.Add($"GameSeries: {gameSeries.DocumentId}");

                var game = await this.ReadGame(gameSeries);
                this.logger.LogInformation($"Game: {game.DocumentId}");
                jsonArray.Add($"Game: {game.DocumentId}");

                var survey = await this.ReadSurvey(game);
                this.logger.LogInformation($"Survey: {survey.DocumentId}");
                jsonArray.Add($"Survey: {survey.DocumentId}");

                await this.SubmitSurveyResults(survey);
                this.logger.LogInformation("survey results submitted");
                jsonArray.Add("survey results submitted");

                var surveyStatus = await this.ReadSurveyStatusClosed(survey);
                this.logger.LogInformation($"survey status: {surveyStatus.DocumentId}");
                jsonArray.Add($"survey status: {surveyStatus.DocumentId}");

                var playerMappings = await this.ReadPlayerMappings(game);
                this.logger.LogInformation($"PlayerMappings: {playerMappings.DocumentId}");
                jsonArray.Add($"PlayerMappings: {playerMappings.DocumentId}");

                await this.TerminateGame(gameSeries, game);
                this.logger.LogInformation("game terminated");
                jsonArray.Add("game terminated");

                var gameStatus = await this.ReadGameStatus(game);
                this.logger.LogInformation($"game status: {gameStatus.DocumentId}");
                jsonArray.Add($"game status: {gameStatus.DocumentId}");

                this.logger.LogInformation("TESTS OK");
                jsonArray.Add("TESTS OK");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Tests failed!");
                jsonArray.Add(ex.Message);
                jsonArray.Add("Tests failed!");
            }

            return new JObject(new JProperty("tests", jsonArray)).ToString();
        }

        /// <summary>
        ///     Initialize a new game series and read the document from the database collection.
        /// </summary>
        /// <param name="externalId">The external id of the game series.</param>
        /// <returns>A <see cref="Task{TResult}" /> whose result is the initialized <see cref="IGameSeries" />.</returns>
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

            foreach (var i in FunctionProvider.Wait())
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

        /// <summary>
        ///     Read the game that is created by process after creating a new game series.
        /// </summary>
        /// <param name="gameSeries">The game series that is created in step <see cref="InitializeGameSeries" />..</param>
        /// <returns>A <see cref="Task{TResult}" /> whose result is the created <see cref="IGame" />.</returns>
        private async Task<IGame> ReadGame(IGameSeries gameSeries)
        {
            foreach (var i in FunctionProvider.Wait())
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

        /// <summary>
        ///     Read the game status that is created after terminating the game, see <see cref="TerminateGame" />.
        /// </summary>
        /// <param name="game">The game from step <see cref="ReadGame" />.</param>
        /// <returns>A <see cref="Task{TResult}" /> whose result is the created <see cref="IGameStatus" />.</returns>
        private async Task<IGameStatus> ReadGameStatus(IGame game)
        {
            foreach (var i in FunctionProvider.Wait())
            {
                var gameStatus = (await this.gameStatusReadOnlyDatabase.ReadManyAsync(
                    DatabaseObject.ParentDocumentIdName,
                    game.DocumentId)).ToArray();
                var status = gameStatus.FirstOrDefault(x => x.Status == Status.Closed);
                if (status == null)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    return status;
                }
            }

            throw new Exception($"Cannot read game status for game {game.DocumentId}");
        }

        /// <summary>
        ///     Read the player mappings that is created after submitting survey votes, see <see cref="SubmitSurveyResults" />.
        /// </summary>
        /// <param name="game">The game from step <see cref="ReadGame" />.</param>
        /// <returns>The created <see cref="IPlayerMappings" />.</returns>
        private async Task<IPlayerMappings> ReadPlayerMappings(IGame game)
        {
            foreach (var i in FunctionProvider.Wait())
            {
                var playerMappings = await this.playerMappingsReadOnlyDatabase.ReadOneAsync(
                    DatabaseObject.ParentDocumentIdName,
                    game.DocumentId);
                if (playerMappings == null)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    return playerMappings;
                }
            }

            throw new Exception($"Cannot read player mappings for game {game.DocumentId}");
        }

        /// <summary>
        ///     Read the survey that is created after creating a new game series, see <see cref="InitializeGameSeries" />.
        /// </summary>
        /// <param name="game">The game from step <see cref="ReadGame" />.</param>
        /// <returns>The created <see cref="IPlayerMappings" />.</returns>
        private async Task<ISurvey> ReadSurvey(IGame game)
        {
            foreach (var i in FunctionProvider.Wait())
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

        /// <summary>
        ///     Read the survey status closed for the survey of step <see cref="ReadSurvey" />.
        /// </summary>
        /// <param name="survey">The survey from step <see cref="ReadSurvey" />.</param>
        /// <returns>The created <see cref="ISurveyStatus" />.</returns>
        private async Task<ISurveyStatus> ReadSurveyStatusClosed(ISurvey survey)
        {
            foreach (var i in FunctionProvider.Wait())
            {
                var surveyStatus = (await this.surveyStatusReadOnlyDatabase.ReadManyAsync(
                    DatabaseObject.ParentDocumentIdName,
                    survey.DocumentId)).ToArray();
                var status = surveyStatus.FirstOrDefault(x => x.Status == Surveys.Common.Contracts.Status.Closed);
                if (status == null)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    return status;
                }
            }

            throw new Exception($"Cannot read survey status closed for survey {survey.DocumentId}");
        }

        /// <summary>
        ///     Submit the survey votes for the survey read in step <see cref="ReadSurvey" />.
        /// </summary>
        /// <param name="survey">The survey for that votes are submitted.</param>
        /// <returns>A <see cref="Task" /> whose result indicates termination.</returns>
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

        /// <summary>
        ///     Send a game termination request for each player.
        /// </summary>
        /// <param name="gameSeries">The game series from step <see cref="InitializeGameSeries" />.</param>
        /// <param name="game">The game from step <see cref="ReadGame" />.</param>
        /// <returns>A <see cref="Task" /> whose result indicates process termination.</returns>
        private async Task TerminateGame(IGameSeries gameSeries, IGame game)
        {
            foreach (var gameTermination in game.GameTerminations)
            {
                await this.startGameTerminationPubSubClient.PublishAsync(
                    new StartGameTerminationMessage(
                        Guid.NewGuid().ToString(),
                        gameSeries.DocumentId,
                        game.DocumentId,
                        gameTermination.TerminationId,
                        gameSeries.Sides.First().Id));
            }
        }

        /// <summary>
        ///     Waiter method.
        /// </summary>
        /// <returns>The current iteration.</returns>
        private static IEnumerable<int> Wait()
        {
            for (var i = 0; i < FunctionProvider.MaxIterations; i++)
            {
                yield return i;
                Thread.Sleep(FunctionProvider.Sleep);
            }
        }
    }
}
