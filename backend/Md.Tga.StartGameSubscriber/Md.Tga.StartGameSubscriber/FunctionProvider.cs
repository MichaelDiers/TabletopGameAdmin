namespace Md.Tga.StartGameSubscriber
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
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
    public class FunctionProvider : PubSubProvider<IStartGameMessage, Function>
    {
        private readonly IGameNameReadOnlyDatabase gameNameReadOnlyDatabase;

        /// <summary>
        ///     Access the database games collection.
        /// </summary>
        private readonly IGameReadOnlyDatabase gameReadOnlyDatabase;

        /// <summary>
        ///     Access to the database collection game-series.
        /// </summary>
        private readonly IGameSeriesReadOnlyDatabase gameSeriesDatabase;

        /// <summary>
        ///     Access pub/sub.
        /// </summary>
        private readonly ISaveGamePubSubClient saveGamePubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gameSeriesDatabase">Access to the database collection game-series.</param>
        /// <param name="gameReadOnlyDatabase">Access the games database collection.</param>
        /// <param name="saveGamePubSubClient">Client for accessing pub/sub.</param>
        /// <param name="gameNameReadOnlyDatabase">Access the game name database collection.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameSeriesReadOnlyDatabase gameSeriesDatabase,
            IGameReadOnlyDatabase gameReadOnlyDatabase,
            ISaveGamePubSubClient saveGamePubSubClient,
            IGameNameReadOnlyDatabase gameNameReadOnlyDatabase
        )
            : base(logger)
        {
            this.gameSeriesDatabase = gameSeriesDatabase ?? throw new ArgumentNullException(nameof(gameSeriesDatabase));
            this.gameReadOnlyDatabase =
                gameReadOnlyDatabase ?? throw new ArgumentNullException(nameof(gameReadOnlyDatabase));
            this.saveGamePubSubClient =
                saveGamePubSubClient ?? throw new ArgumentNullException(nameof(saveGamePubSubClient));
            this.gameNameReadOnlyDatabase = gameNameReadOnlyDatabase;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IStartGameMessage message)
        {
            var (gameSeries, gameName) = await this.ReadDataAsync(message.GameSeriesDocumentId);
            var game = new Game(
                null,
                null,
                gameSeries.DocumentId,
                gameName,
                gameSeries.Players.Select(player => new GameTermination(player.Id, Guid.NewGuid().ToString()))
                    .ToArray());

            await this.saveGamePubSubClient.PublishAsync(new SaveGameMessage(message.ProcessId, gameSeries, game));
        }

        /// <summary>
        ///     Read the game series by document id.
        /// </summary>
        /// <param name="gameSeriesDocumentId">The document id of the game series.</param>
        /// <returns>An <see cref="IGameSeries" />.</returns>
        private async Task<(IGameSeries gameSeries, string gameName)> ReadDataAsync(string gameSeriesDocumentId)
        {
            var gameSeriesTask = this.gameSeriesDatabase.ReadByDocumentIdAsync(gameSeriesDocumentId);
            var gameNumberTask = this.gameReadOnlyDatabase.CountGames(gameSeriesDocumentId);

            var gameSeries = await gameSeriesTask;
            if (gameSeries == null)
            {
                throw new ArgumentException($"No game series with id {gameSeriesDocumentId} found.");
            }

            var gameNumber = await gameNumberTask;

            var gameName = await this.gameNameReadOnlyDatabase.ReadByDocumentIdAsync(gameNumber.ToString());

            return (gameSeries, gameName == null ? $"GameName-{gameNumber}" : gameName.Name);
        }
    }
}
