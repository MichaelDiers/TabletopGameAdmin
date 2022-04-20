namespace Md.Tga.SaveGameTerminationSubscriber
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
    public class FunctionProvider : PubSubProvider<ISaveGameTerminationResultMessage, Function>
    {
        private readonly IEvaluateGameTerminationPubSubClient evaluateGameTerminationPubSubClient;

        /// <summary>
        ///     Access the results of game termination requests.
        /// </summary>
        private readonly IGameTerminationResultDatabase gameTerminationResultDatabase;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gameTerminationResultDatabase">Access to the database collection game-termination-results.</param>
        /// <param name="evaluateGameTerminationPubSubClient">Send game termination evaluation requests.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameTerminationResultDatabase gameTerminationResultDatabase,
            IEvaluateGameTerminationPubSubClient evaluateGameTerminationPubSubClient
        )
            : base(logger)
        {
            this.gameTerminationResultDatabase = gameTerminationResultDatabase;
            this.evaluateGameTerminationPubSubClient = evaluateGameTerminationPubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(ISaveGameTerminationResultMessage message)
        {
            await this.gameTerminationResultDatabase.InsertAsync(
                Guid.NewGuid().ToString(),
                message.GameTerminationResult);
            await this.evaluateGameTerminationPubSubClient.PublishAsync(
                new EvaluateGameTerminationMessage(message.ProcessId, message.GameTerminationResult.ParentDocumentId));
        }
    }
}
