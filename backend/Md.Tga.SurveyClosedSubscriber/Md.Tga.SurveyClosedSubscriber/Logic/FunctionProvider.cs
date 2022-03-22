namespace Md.Tga.InitializeGameSeriesSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.InitializeGameSeriesSubscriber.Contracts;
    using Md.Tga.SurveyClosedSubscriber.Contracts;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts.Messages;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ISurveyClosedMessage, Function>
    {
        private readonly IGamesDatabase gamesDatabase;
        private readonly IGameSeriesDatabase gameSeriesDatabase;
        private readonly ISaveGameSeriesPubSubClient saveGameSeriesPubSubClient;
        private readonly IStartGamePubSubClient startGamePubSubClient;
        private readonly ISurveyEvaluator surveyEvaluator;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gamesDatabase"></param>
        /// <param name="saveGameSeriesPubSubClient">Pub/sub client for saving a game series.</param>
        /// <param name="startGamePubSubClient">Pub/sub client for starting a new game.</param>
        /// <param name="gameSeriesDatabase"></param>
        /// <param name="surveyEvaluator"></param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameSeriesDatabase gameSeriesDatabase,
            IGamesDatabase gamesDatabase,
            ISaveGameSeriesPubSubClient saveGameSeriesPubSubClient,
            IStartGamePubSubClient startGamePubSubClient,
            ISurveyEvaluator surveyEvaluator
        )
            : base(logger)
        {
            this.gameSeriesDatabase = gameSeriesDatabase;
            this.gamesDatabase = gamesDatabase;
            this.saveGameSeriesPubSubClient = saveGameSeriesPubSubClient ??
                                              throw new ArgumentNullException(nameof(saveGameSeriesPubSubClient));
            this.startGamePubSubClient =
                startGamePubSubClient ?? throw new ArgumentNullException(nameof(startGamePubSubClient));
            this.surveyEvaluator = surveyEvaluator;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(ISurveyClosedMessage message)
        {
            var game = await this.gamesDatabase.ReadGameBySurveyId(message.Survey.Id);
            var gameSeries = await this.gameSeriesDatabase.ReadById(game.InternalGameSeriesId);

            var solution = this.surveyEvaluator.Evaluate(gameSeries, message.Results);

            // save game
            // create mails
        }

        private void FindSolution(IGameSeries gameSeries, IGame game, ISurveyClosedMessage surveyClosedMessage)
        {
        }
    }
}
