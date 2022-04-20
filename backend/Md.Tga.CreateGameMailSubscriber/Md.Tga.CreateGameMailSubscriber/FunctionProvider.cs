namespace Md.Tga.CreateGameMailSubscriber
{
    using System;
    using System.Linq;
    using System.Text;
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
            switch (message.GameMailType)
            {
                case GameMailType.None:
                    throw new ArgumentException(
                        $"Cannot handle mail type {message.GameMailType}",
                        nameof(message.GameMailType));
                case GameMailType.SurveyResult:
                    await this.HandleMessageSurveyResultAsync(message);
                    break;
                case GameMailType.GameTerminationUpdate:
                    await this.HandleMessageGameTerminationUpdateAsync(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task HandleMessageGameTerminationUpdateAsync(ICreateGameMailMessage message)
        {
            var htmlResult = new StringBuilder();
            var textResult = new StringBuilder();
            foreach (var gameSeriesSide in message.GameSeries.Sides)
            {
                var count = message.GameTerminationResults.Count(gtr => gtr.WinningSideId == gameSeriesSide.Id);
                htmlResult.AppendFormat(GameTerminationUpdateText.BodyHtmlResult, gameSeriesSide.Name, count);
                textResult.AppendFormat(GameTerminationUpdateText.BodyTextResult, gameSeriesSide.Name, count);
            }

            foreach (var player in message.GameSeries.Players)
            {
                var sendMailMessage = new SendMailMessage(
                    message.ProcessId,
                    new[] {new Recipient(player.Email, player.Name)},
                    new Recipient(message.GameSeries.Organizer.Email, message.GameSeries.Organizer.Name),
                    GameTerminationUpdateText.Subject,
                    new Body(
                        string.Format(
                            GameTerminationUpdateText.BodyHtml,
                            player.Name,
                            message.Game.Name,
                            htmlResult,
                            message.GameSeries.Organizer.Name),
                        string.Format(
                            GameTerminationUpdateText.BodyText,
                            player.Name,
                            message.Game.Name,
                            textResult,
                            message.GameSeries.Organizer.Name)));
                await this.sendMailPubSubClient.PublishAsync(sendMailMessage);
            }
        }

        private async Task HandleMessageSurveyResultAsync(ICreateGameMailMessage message)
        {
            var htmlResult = new StringBuilder();
            var textResult = new StringBuilder();
            foreach (var gameSeriesCountry in message.GameSeries.Countries)
            {
                var mapping =
                    message.PlayerMappings.PlayerCountryMappings.First(pcm => pcm.CountryId == gameSeriesCountry.Id);
                var player = message.GameSeries.Players.First(player => player.Id == mapping.PlayerId);
                htmlResult.AppendFormat(SurveyResultText.BodyHtmlEntry, gameSeriesCountry.Name, player.Name);
                textResult.AppendFormat(SurveyResultText.BodyTextEntry, gameSeriesCountry.Name, player.Name);
            }

            foreach (var gameSeriesPlayer in message.GameSeries.Players)
            {
                var sendMailMessage = new SendMailMessage(
                    message.ProcessId,
                    new[] {new Recipient(gameSeriesPlayer.Email, gameSeriesPlayer.Name)},
                    new Recipient(message.GameSeries.Organizer.Email, message.GameSeries.Organizer.Name),
                    string.Format(SurveyResultText.Subject, message.Game.Name),
                    new Body(
                        string.Format(
                            SurveyResultText.BodyHtml,
                            gameSeriesPlayer.Name,
                            htmlResult,
                            message.GameSeries.Organizer.Name),
                        string.Format(
                            SurveyResultText.BodyText,
                            gameSeriesPlayer.Name,
                            textResult,
                            message.GameSeries.Organizer.Name)));
                await this.sendMailPubSubClient.PublishAsync(sendMailMessage);
            }
        }
    }
}
