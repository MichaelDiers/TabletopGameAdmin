namespace Md.Tga.EvaluateGameTerminationSubscriber
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
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IEvaluateGameTerminationMessage, Function>
    {
        private readonly ICreateGameMailPubSubClient createGameMailPubSubClient;

        /// <summary>
        ///     Access the database games collection.
        /// </summary>
        private readonly IGameReadOnlyDatabase gameReadOnlyDatabase;

        /// <summary>
        ///     Access to the database collection game-series.
        /// </summary>
        private readonly IGameSeriesReadOnlyDatabase gameSeriesReadOnlyDatabase;

        /// <summary>
        ///     Access to the database collection game-termination-result.
        /// </summary>
        private readonly IGameTerminationResultReadOnlyDatabase gameTerminationResultReadOnlyDatabase;

        private readonly IPlayerMappingsReadOnlyDatabase playerMappingsReadOnlyDatabase;
        private readonly ISaveGameStatusPubSubClient saveGameStatusPubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gameTerminationResultReadOnlyDatabase">Access the game termination results.</param>
        /// <param name="gameSeriesDatabase">Access to the database collection game-series.</param>
        /// <param name="gameReadOnlyDatabase">Access the games database collection.</param>
        /// <param name="playerMappingsReadOnlyDatabase">Read the player mappings of the game.</param>
        /// <param name="createGameMailPubSubClient">Create a new email.</param>
        /// <param name="saveGameStatusPubSubClient"></param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameTerminationResultReadOnlyDatabase gameTerminationResultReadOnlyDatabase,
            IGameSeriesReadOnlyDatabase gameSeriesDatabase,
            IGameReadOnlyDatabase gameReadOnlyDatabase,
            IPlayerMappingsReadOnlyDatabase playerMappingsReadOnlyDatabase,
            ICreateGameMailPubSubClient createGameMailPubSubClient,
            ISaveGameStatusPubSubClient saveGameStatusPubSubClient
        )
            : base(logger)
        {
            this.gameTerminationResultReadOnlyDatabase = gameTerminationResultReadOnlyDatabase;
            this.gameSeriesReadOnlyDatabase =
                gameSeriesDatabase ?? throw new ArgumentNullException(nameof(gameSeriesDatabase));
            this.gameReadOnlyDatabase =
                gameReadOnlyDatabase ?? throw new ArgumentNullException(nameof(gameReadOnlyDatabase));
            this.playerMappingsReadOnlyDatabase = playerMappingsReadOnlyDatabase;
            this.createGameMailPubSubClient = createGameMailPubSubClient;
            this.saveGameStatusPubSubClient = saveGameStatusPubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IEvaluateGameTerminationMessage message)
        {
            var data = await this.ReadData(message.GameDocumentId);
            if (data == null)
            {
                return;
            }

            var (gameSeries, game, playerMappings, results) = data.Value;
            var playerResults = results.GroupBy(result => result.PlayerId)
                .Select(group => group.OrderByDescending(x => x.Created).First())
                .ToArray();

            var (winningSideId, rounds) = FunctionProvider.EvaluateGameTerminations(gameSeries, playerResults);
            if (!string.IsNullOrWhiteSpace(winningSideId))
            {
                await this.saveGameStatusPubSubClient.PublishAsync(
                    new SaveGameStatusMessage(
                        message.ProcessId,
                        new GameStatus(
                            null,
                            DateTime.Now,
                            game.DocumentId,
                            Status.Closed,
                            winningSideId,
                            rounds),
                        new CreateGameMailMessage(
                            message.ProcessId,
                            GameMailType.GameTerminated,
                            gameSeries,
                            game,
                            playerMappings,
                            playerResults)));
            }
            else
            {
                await this.createGameMailPubSubClient.PublishAsync(
                    new CreateGameMailMessage(
                        message.ProcessId,
                        GameMailType.GameTerminationUpdate,
                        gameSeries,
                        game,
                        playerMappings,
                        playerResults.Select(x => x)));
            }
        }

        /// <summary>
        ///     Check if the game can be terminated.
        /// </summary>
        /// <param name="gameSeries">The game is part of that game series.</param>
        /// <param name="results">The game termination results.</param>
        /// <returns>The winning side id if there is a winning side and an emptry string otherwise.</returns>
        private static (string winningSideId, int rounds) EvaluateGameTerminations(
            IGameSeries gameSeries,
            IList<IGameTerminationResult> results
        )
        {
            var sorted = results.OrderByDescending(result => result.Created).ToArray();
            var gameTerminationResults = gameSeries.Players
                .Select(player => sorted.FirstOrDefault(result => result.PlayerId == player.Id))
                .Where(x => x != null)
                .ToArray();

            if (!gameTerminationResults.Any())
            {
                return (string.Empty, 0);
            }

            if (gameTerminationResults.Select(x => x.WinningSideId).Distinct().Count() != 1)
            {
                return (string.Empty, 0);
            }

            if (gameSeries.Players.Count() != gameTerminationResults.Length)
            {
                return (string.Empty, 0);
            }

            var winningSide =
                gameTerminationResults.FirstOrDefault(result => result?.PlayerId == gameSeries.Organizer.Id);
            if (winningSide != null)
            {
                return (winningSide.WinningSideId, winningSide.Rounds);
            }

            return (gameTerminationResults.First().WinningSideId, 0);
        }

        /// <summary>
        ///     Read the data for processing.
        /// </summary>
        /// <param name="gameId">The id of the game.</param>
        /// <returns>A tuple that contains the requested data.</returns>
        private async
            Task<(IGameSeries gameSeries, IGame game, IPlayerMappings playerMappings, IList<IGameTerminationResult>
                results)?> ReadData(string gameId)
        {
            var gameTask = this.gameReadOnlyDatabase.ReadByDocumentIdAsync(gameId);
            var resultsTask = this.gameTerminationResultReadOnlyDatabase.ReadManyAsync(
                DatabaseObject.ParentDocumentIdName,
                gameId);
            var playerMappingsTask = this.playerMappingsReadOnlyDatabase.ReadOneAsync(
                DatabaseObject.ParentDocumentIdName,
                gameId);

            var game = await gameTask;
            if (game == null)
            {
                await this.LogErrorAsync($"No game found for id {gameId}");
                return null;
            }

            var gameSeries = await this.gameSeriesReadOnlyDatabase.ReadByDocumentIdAsync(game.ParentDocumentId);
            if (gameSeries == null)
            {
                await this.LogErrorAsync($"No game series found for game id {gameId}");
                return null;
            }

            var playerMappings = await playerMappingsTask;
            if (playerMappings == null)
            {
                await this.LogErrorAsync($"No player mappings found for game id {gameId}");
                return null;
            }

            return (gameSeries, game, playerMappings, (await resultsTask).ToList());
        }
    }
}
