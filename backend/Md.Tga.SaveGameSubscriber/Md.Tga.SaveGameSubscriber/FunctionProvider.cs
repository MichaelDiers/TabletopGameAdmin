namespace Md.Tga.SaveGameSubscriber
{
    using System;
    using System.Threading.Tasks;
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
    public class FunctionProvider : PubSubProvider<ISaveGameMessage, Function>
    {
        /// <summary>
        ///     Access the database.
        /// </summary>
        private readonly IGameDatabase database;

        private readonly IStartSurveyPubSubClient startSurveyPubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the database.</param>
        /// <param name="startSurveyPubSubClient">Start a new survey.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameDatabase database,
            IStartSurveyPubSubClient startSurveyPubSubClient
        )
            : base(logger)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.startSurveyPubSubClient = startSurveyPubSubClient ??
                                           throw new ArgumentNullException(nameof(startSurveyPubSubClient));
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(ISaveGameMessage message)
        {
            var game = new Game(message.GameSeries, message.Game);
            await this.database.InsertAsync(game.DocumentId, game);
            var startSurveyMessage = new StartSurveyMessage(message.ProcessId, message.GameSeries, message.Game);
            await this.startSurveyPubSubClient.PublishAsync(startSurveyMessage);
        }
    }
}
