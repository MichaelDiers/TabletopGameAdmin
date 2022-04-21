namespace Md.Tga.SaveGameStatusSubscriber
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloudFunctions.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ISaveGameStatusMessage, Function>
    {
        private readonly ICreateGameMailPubSubClient createGameMailPubSubClient;

        /// <summary>
        ///     Access the database.
        /// </summary>
        private readonly IGameStatusDatabase gameStatusDatabase;

        private readonly IStartGamePubSubClient startGamePubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gameStatusDatabase">Access to the database.</param>
        /// <param name="createGameMailPubSubClient">Start a new survey.</param>
        /// <param name="startGamePubSubClient"></param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameStatusDatabase gameStatusDatabase,
            ICreateGameMailPubSubClient createGameMailPubSubClient,
            IStartGamePubSubClient startGamePubSubClient
        )
            : base(logger)
        {
            this.gameStatusDatabase = gameStatusDatabase ?? throw new ArgumentNullException(nameof(gameStatusDatabase));
            this.createGameMailPubSubClient = createGameMailPubSubClient ??
                                              throw new ArgumentNullException(nameof(createGameMailPubSubClient));
            this.startGamePubSubClient = startGamePubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(ISaveGameStatusMessage message)
        {
            await this.gameStatusDatabase.InsertAsync(Guid.NewGuid().ToString(), message.GameStatus);

            if (message.CreateGameMailMessage != null)
            {
                await this.createGameMailPubSubClient.PublishAsync(message.CreateGameMailMessage);
                if (message.GameStatus.Status == Status.Closed)
                {
                    await this.startGamePubSubClient.PublishAsync(
                        new StartGameMessage(message.ProcessId, message.CreateGameMailMessage.GameSeries.DocumentId));
                }
            }
        }
    }
}
