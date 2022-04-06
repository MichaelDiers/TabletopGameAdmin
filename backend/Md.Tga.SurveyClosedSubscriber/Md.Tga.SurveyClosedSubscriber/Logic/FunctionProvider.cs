namespace Md.Tga.SurveyClosedSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Md.Tga.SurveyClosedSubscriber.Contracts;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts.Messages;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ISurveyClosedMessage, Function>
    {
        private readonly IGameReadOnlyDatabase gamesDatabase;
        private readonly IGameSeriesReadOnlyDatabase gameSeriesDatabase;
        private readonly ISavePlayerMappingsPubSubClient savePlayerMappingsPubSubClient;
        private readonly ISurveyEvaluator surveyEvaluator;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gamesDatabase"></param>
        /// <param name="gameSeriesDatabase"></param>
        /// <param name="surveyEvaluator"></param>
        /// <param name="savePlayerMappingsPubSubClient">Client for publishing save player mappings.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameSeriesReadOnlyDatabase gameSeriesDatabase,
            IGameReadOnlyDatabase gamesDatabase,
            ISurveyEvaluator surveyEvaluator,
            ISavePlayerMappingsPubSubClient savePlayerMappingsPubSubClient
        )
            : base(logger)
        {
            this.gameSeriesDatabase = gameSeriesDatabase;
            this.gamesDatabase = gamesDatabase;

            this.surveyEvaluator = surveyEvaluator;
            this.savePlayerMappingsPubSubClient = savePlayerMappingsPubSubClient;
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
            // create mails
        }
    }
}
