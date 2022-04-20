namespace Md.Tga.SavePlayerMappingsSubscriber
{
    using System;
    using System.Linq;
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
    public class FunctionProvider : PubSubProvider<ISavePlayerMappingsMessage, Function>
    {
        private readonly ICreateGameMailPubSubClient createGameMailPubSubClient;

        /// <summary>
        ///     Access the database.
        /// </summary>
        private readonly IPlayerMappingsDatabase database;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the database.</param>
        /// <param name="createGameMailPubSubClient">Access to the pub/sub client.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IPlayerMappingsDatabase database,
            ICreateGameMailPubSubClient createGameMailPubSubClient
        )
            : base(logger)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.createGameMailPubSubClient = createGameMailPubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(ISavePlayerMappingsMessage message)
        {
            await this.database.InsertAsync(Guid.NewGuid().ToString(), message.PlayerMappings);
            await this.createGameMailPubSubClient.PublishAsync(
                new CreateGameMailMessage(
                    message.ProcessId,
                    GameMailType.SurveyResult,
                    message.GameSeries,
                    message.Game,
                    message.PlayerMappings,
                    Enumerable.Empty<IGameTerminationResult>()));
        }
    }
}
