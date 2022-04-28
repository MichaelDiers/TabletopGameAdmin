namespace Md.Tga.StartGameTerminationSubscriber
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.Common.Logic;
    using Md.GoogleCloudFunctions.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IStartGameTerminationMessage, Function>
    {
        /// <summary>
        ///     Access the database games collection.
        /// </summary>
        private readonly IGameReadOnlyDatabase gameReadOnlyDatabase;

        /// <summary>
        ///     Access to the database collection game-series.
        /// </summary>
        private readonly IGameSeriesReadOnlyDatabase gameSeriesReadOnlyDatabase;

        private readonly ISaveGameTerminationResultPubSubClient gameTerminationResultPubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gameSeriesDatabase">Access to the database collection game-series.</param>
        /// <param name="gameReadOnlyDatabase">Access the games database collection.</param>
        /// <param name="gameTerminationResultPubSubClient">Client for saving game termination results.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameSeriesReadOnlyDatabase gameSeriesDatabase,
            IGameReadOnlyDatabase gameReadOnlyDatabase,
            ISaveGameTerminationResultPubSubClient gameTerminationResultPubSubClient
        )
            : base(logger)
        {
            this.gameSeriesReadOnlyDatabase =
                gameSeriesDatabase ?? throw new ArgumentNullException(nameof(gameSeriesDatabase));
            this.gameReadOnlyDatabase =
                gameReadOnlyDatabase ?? throw new ArgumentNullException(nameof(gameReadOnlyDatabase));
            this.gameTerminationResultPubSubClient = gameTerminationResultPubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IStartGameTerminationMessage message)
        {
            var gameSeriesTask = this.gameSeriesReadOnlyDatabase.ReadByDocumentIdAsync(message.GameSeriesDocumentId);
            var gameTask = this.gameReadOnlyDatabase.ReadByDocumentIdAsync(message.GameDocumentId);

            var gameSeries = await gameSeriesTask;
            if (gameSeries == null)
            {
                await this.LogErrorAsync($"Cannot read game series data: {Serializer.SerializeObject(message)}");
                return;
            }

            if (gameSeries.Sides.All(side => side.Id != message.WinningSideId))
            {
                await this.LogErrorAsync($"Unknown side id: {Serializer.SerializeObject(message)}");
                return;
            }

            var game = await gameTask;
            if (game == null)
            {
                await this.LogErrorAsync($"Cannot read game data: {Serializer.SerializeObject(message)}");
                return;
            }

            var gameTermination =
                game.GameTerminations.FirstOrDefault(termination => termination.TerminationId == message.TerminationId);
            if (gameTermination == null)
            {
                await this.LogErrorAsync(
                    $"Game does not include termination id: {Serializer.SerializeObject(message)}");
                return;
            }

            var player = gameSeries.Players.FirstOrDefault(player => player.Id == gameTermination.PlayerId);
            if (player == null)
            {
                await this.LogErrorAsync(
                    $"Data mismatch: No player found in game series: {Serializer.SerializeObject(message)}");
                return;
            }

            var gameTerminationResult = new GameTerminationResult(
                null,
                null,
                game.DocumentId,
                player.Id,
                message.WinningSideId,
                message.Reason,
                message.Rounds);

            await this.gameTerminationResultPubSubClient.PublishAsync(
                new SaveGameTerminationResultMessage(message.ProcessId, gameTerminationResult));
        }
    }
}
