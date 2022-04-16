namespace Md.Tga.StartGameSeriesSubscriber
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloudFunctions.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IStartGameSeriesMessage, Function>
    {
        /// <summary>
        ///     Access the database.
        /// </summary>
        private readonly IGameConfigReadOnlyDatabase database;

        /// <summary>
        ///     Send messages to to pub/sub.
        /// </summary>
        private readonly ISaveGameSeriesPubSubClient pubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the database.</param>
        /// <param name="pubSubClient">Access to pub/sub.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameConfigReadOnlyDatabase database,
            ISaveGameSeriesPubSubClient pubSubClient
        )
            : base(logger)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.pubSubClient = pubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IStartGameSeriesMessage message)
        {
            var gameConfig = await this.database.ReadByDocumentIdAsync(message.GameSeries.GameType.ToUpperInvariant());
            if (gameConfig == null)
            {
                await this.LogErrorAsync(
                    new Exception(),
                    $"No config found for document id {message.GameSeries.GameType.ToUpperInvariant()}");
                return;
            }

            var gameSeries = FunctionProvider.InitializeGameSeries(message, gameConfig);

            var saveGameSeriesMessage = new SaveGameSeriesMessage(message.ProcessId, gameSeries);
            await this.pubSubClient.PublishAsync(saveGameSeriesMessage);
        }

        /// <summary>
        ///     Initialize a new game series.
        /// </summary>
        /// <param name="message">The incoming pub/sub message.</param>
        /// <param name="gameConfig">The game configuration.</param>
        /// <returns>An instance of <see cref="IGameSeries" />.</returns>
        private static IGameSeries InitializeGameSeries(IStartGameSeriesMessage message, IGameConfig gameConfig)
        {
            var sides = gameConfig.Countries.Select(country => country.Side)
                .Distinct()
                .Select(side => new Side(Guid.NewGuid().ToString(), side))
                .ToArray();
            var countries = gameConfig.Countries.Select(
                    country => new Country(
                        Guid.NewGuid().ToString(),
                        country.Name,
                        sides.First(side => side.Name == country.Side).Id))
                .ToArray();
            var organizer = new Person(
                Guid.NewGuid().ToString(),
                message.GameSeries.Organizer.Name,
                message.GameSeries.Organizer.Email);
            var players = message.GameSeries.Players.Select(
                    player => new Person(Guid.NewGuid().ToString(), player.Name, player.Email))
                .ToArray();

            return new GameSeries(
                null,
                null,
                message.GameSeries.ExternalId,
                gameConfig.Name,
                message.GameSeries.Name,
                sides,
                countries,
                organizer,
                players,
                message.GameSeries.GameType);
        }
    }
}
