namespace Md.Tga.CreateGameMailSubscriber
{
    using System.Threading.Tasks;
    using Md.GoogleCloudFunctions.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ICreateGameMailMessage, Function>
    {
        /// <summary>
        ///     Access pub/sub for sending messages.
        /// </summary>
        private readonly ISendMailPubSubClient sendMailPubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="sendMailPubSubClient">Send a new email.</param>
        public FunctionProvider(ILogger<Function> logger, ISendMailPubSubClient sendMailPubSubClient)
            : base(logger)
        {
            this.sendMailPubSubClient = sendMailPubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(ICreateGameMailMessage message)
        {
            foreach (var gameSeriesPlayer in message.GameSeries.Players)
            {
                var sendMailMessage = new SendMailMessage(
                    message.ProcessId,
                    new[] {new Recipient(gameSeriesPlayer.Email, gameSeriesPlayer.Name)},
                    new Recipient(message.GameSeries.Organizer.Email, message.GameSeries.Organizer.Name),
                    "subject",
                    new Body("html", "text"));
                await this.sendMailPubSubClient.PublishAsync(sendMailMessage);
            }
        }
    }
}
