namespace Md.Tga.SaveGameSeriesSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
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
        private readonly IPubSubClient pubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the database.</param>
        /// <param name="pubSubClient">Access to pub/sub.</param>
        public FunctionProvider(ILogger<Function> logger, IGameSeriesDatabase database, IPubSubClient pubSubClient)
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
            var internalId = Guid.NewGuid().ToString();
            await this.database.InsertAsync(internalId, message.GameSeries);
            await this.pubSubClient.PublishAsync(new StartGameMessage(message.ProcessId, internalId));
        }
    }
}
