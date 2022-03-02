namespace Md.Tga.InitializeGameSeriesSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Md.Tga.InitializeGameSeriesSubscriber.Contracts;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IInitializeGameSeriesMessage, Function>
    {
        private readonly ISaveGameSeriesPubSubClient saveGameSeriesPubSubClient;
        private readonly IStartGamePubSubClient startGamePubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="saveGameSeriesPubSubClient">Pub/sub client for saving a game series.</param>
        /// <param name="startGamePubSubClient">Pub/sub client for starting a new game.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            ISaveGameSeriesPubSubClient saveGameSeriesPubSubClient,
            IStartGamePubSubClient startGamePubSubClient
        )
            : base(logger)
        {
            this.saveGameSeriesPubSubClient = saveGameSeriesPubSubClient
                                              ?? throw new ArgumentNullException(nameof(saveGameSeriesPubSubClient));
            this.startGamePubSubClient =
                startGamePubSubClient ?? throw new ArgumentNullException(nameof(startGamePubSubClient));
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IInitializeGameSeriesMessage message)
        {
            var internalId = Guid.NewGuid().ToString();

            await this.saveGameSeriesPubSubClient.PublishAsync(
                new SaveGameSeriesMessage(message.ProcessId, message.GameSeries, internalId));

            await this.startGamePubSubClient.PublishAsync(
                new StartGameMessage(message.ProcessId, message.GameSeries, internalId));
        }
    }
}
