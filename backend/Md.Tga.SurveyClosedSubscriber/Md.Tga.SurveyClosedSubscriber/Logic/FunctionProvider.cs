namespace Md.Tga.SurveyClosedSubscriber.Logic
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloudFunctions.Logic;
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
            var game = await this.gamesDatabase.ReadByDocumentIdAsync(message.Survey.ParentDocumentId);
            if (game == null)
            {
                throw new ArgumentException($"No game found for survey id {message.Survey.ParentDocumentId}");
            }

            var gameSeries = await this.gameSeriesDatabase.ReadByDocumentIdAsync(game.ParentDocumentId);
            if (gameSeries == null)
            {
                await this.LogErrorAsync(
                    new Exception(),
                    $"No game series with document id {game.ParentDocumentId} found");
                return;
            }

            var solution = this.surveyEvaluator.Evaluate(gameSeries, message.Results).ToArray();

            await this.savePlayerMappingsPubSubClient.PublishAsync(
                new SavePlayerMappingsMessage(
                    message.ProcessId,
                    gameSeries,
                    game,
                    new PlayerMappings(
                        null,
                        null,
                        game.DocumentId,
                        solution)));
        }
    }
}
