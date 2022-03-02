namespace Md.Tga.StartGameSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.StartGameSubscriber.Contracts;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IStartGameMessage, Function>
    {
        /// <summary>
        ///     Access to the database collection games.
        /// </summary>
        private readonly IReadOnlyDatabase gamesDatabase;

        /// <summary>
        ///     Access to the database collection game-series.
        /// </summary>
        private readonly IReadOnlyDatabase gameSeriesDatabase;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gameSeriesDatabase">Access to the database collection game-series.</param>
        /// <param name="gamesDatabase">Access to the database collection games.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameSeriesReadOnlyDatabase gameSeriesDatabase,
            IGamesReadOnlyDatabase gamesDatabase
        )
            : base(logger)
        {
            this.gameSeriesDatabase = gameSeriesDatabase ?? throw new ArgumentNullException(nameof(gameSeriesDatabase));
            this.gamesDatabase = gamesDatabase ?? throw new ArgumentNullException(nameof(gamesDatabase));
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override Task HandleMessageAsync(IStartGameMessage message)
        {
            //await this.database.InsertAsync(message.InternalId, message.GameSeries);
            return Task.CompletedTask;
        }
    }
}
