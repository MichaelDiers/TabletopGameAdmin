namespace Md.Tga.SurveyClosedSubscriber.Logic
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Md.Tga.SurveyClosedSubscriber.Contracts;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ISurveyClosedMessage, Function>
    {
        private readonly IGameReadOnlyDatabase gamesDatabase;
        private readonly IGameSeriesReadOnlyDatabase gameSeriesDatabase;
        private readonly ISavePlayerMappingsPubSubClient savePlayerMappingsPubSubClient;
        private readonly ISendMailPubSubClient sendMailPubSubClient;
        private readonly ISurveyEvaluator surveyEvaluator;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gamesDatabase"></param>
        /// <param name="gameSeriesDatabase"></param>
        /// <param name="surveyEvaluator"></param>
        /// <param name="savePlayerMappingsPubSubClient">Client for publishing save player mappings.</param>
        /// <param name="sendMailPubSubClient">Client publishing send mail messages.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameSeriesReadOnlyDatabase gameSeriesDatabase,
            IGameReadOnlyDatabase gamesDatabase,
            ISurveyEvaluator surveyEvaluator,
            ISavePlayerMappingsPubSubClient savePlayerMappingsPubSubClient,
            ISendMailPubSubClient sendMailPubSubClient
        )
            : base(logger)
        {
            this.gameSeriesDatabase = gameSeriesDatabase;
            this.gamesDatabase = gamesDatabase;

            this.surveyEvaluator = surveyEvaluator;
            this.savePlayerMappingsPubSubClient = savePlayerMappingsPubSubClient;
            this.sendMailPubSubClient = sendMailPubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(ISurveyClosedMessage message)
        {
            var game = await this.gamesDatabase.ReadOneAsync(Game.SurveyIdName, message.Survey.Id);
            if (game == null)
            {
                throw new ArgumentException($"No game found for survey id {message.Survey.Id}");
            }

            var gameSeries = await this.gameSeriesDatabase.ReadByDocumentIdAsync(game.InternalGameSeriesId);

            var solution = this.surveyEvaluator.Evaluate(gameSeries, message.Results);

            await this.savePlayerMappingsPubSubClient.PublishAsync(
                new SavePlayerMappingsMessage(
                    message.ProcessId,
                    new PlayerMappings(game.InternalDocumentId, solution)));

            await this.SendMail(message, gameSeries, game);
        }

        private async Task SendMail(ISurveyClosedMessage message, IGameSeries gameSeries, IGame game)
        {
            var sendMailMessage = new SendMailMessage(
                message.ProcessId,
                gameSeries.Players.Select(p => new Recipient(p.Email, p.Name)).ToArray(),
                new Recipient(gameSeries.Organizer.Email, gameSeries.Organizer.Name),
                "subject",
                new Body("html", "text"),
                game.SurveyId,
                gameSeries.Players.Select(p => p.Id),
                Status.InvitationMailSentOk,
                Status.InvitationMailSentFailed);
            await this.sendMailPubSubClient.PublishAsync(sendMailMessage);
        }
    }
}
