namespace Md.Tga.SaveGameSeriesSubscriber
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloudFunctions.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ISaveGameSeriesMessage, Function>
    {
        /// <summary>
        ///     Access the database.
        /// </summary>
        private readonly IGameSeriesDatabase database;

        /// <summary>
        ///     Send messages to to pub/sub.
        /// </summary>
        private readonly IStartGamePubSubClient pubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the database.</param>
        /// <param name="pubSubClient">Access to pub/sub.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameSeriesDatabase database,
            IStartGamePubSubClient pubSubClient
        )
            : base(logger)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.pubSubClient = pubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(ISaveGameSeriesMessage message)
        {
            var documentId = Guid.NewGuid().ToString();
            await this.database.InsertAsync(documentId, message.GameSeries);
            await this.pubSubClient.PublishAsync(new StartGameMessage(message.ProcessId, documentId));
        }
    }
}
