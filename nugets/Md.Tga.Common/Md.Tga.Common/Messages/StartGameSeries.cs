namespace Md.Tga.Common.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Messages;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a new game series.
    /// </summary>
    public class StartGameSeries : IStartGameSeries
    {
        /// <summary>
        ///     Creates a new instance of <see cref="StartGameSeries" />.
        /// </summary>
        /// <param name="externalId">The external id set by the client for this game series.</param>
        /// <param name="name">The name of the game series.</param>
        /// <param name="gameType">The type of the game series.</param>
        /// <param name="organizer">The organizer of the game series.</param>
        /// <param name="players">The players of the game series.</param>
        public StartGameSeries(
            string externalId,
            string name,
            string gameType,
            StartGameSeriesPerson organizer,
            IEnumerable<StartGameSeriesPerson> players
        )
            : this(
                externalId,
                name,
                gameType,
                organizer,
                players.Select(p => p as IStartGameSeriesPerson))
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="StartGameSeries" />.
        /// </summary>
        /// <param name="externalId">The external id set by the client for this game series.</param>
        /// <param name="name">The name of the game series.</param>
        /// <param name="gameType">The type of the game series.</param>
        /// <param name="organizer">The organizer of the game series.</param>
        /// <param name="players">The players of the game series.</param>
        public StartGameSeries(
            string externalId,
            string name,
            string gameType,
            IStartGameSeriesPerson organizer,
            IEnumerable<IStartGameSeriesPerson> players
        )
        {
            this.ExternalId = externalId.ValidateIsAGuid(nameof(externalId));
            this.Name = name.ValidateIsNotNullOrWhitespace(nameof(name));
            this.GameType = gameType.ValidateIsNotNullOrWhitespace(nameof(gameType));
            this.Organizer = organizer ?? throw new ArgumentNullException(nameof(organizer));
            this.Players = players.ToArray();
        }

        /// <summary>
        ///     Gets the id that is set by the client for the new game series.
        /// </summary>
        [JsonProperty("externalId", Required = Required.Always, Order = 1)]
        public string ExternalId { get; }

        /// <summary>
        ///     Gets the type of the game.
        /// </summary>
        [JsonProperty("gameType", Required = Required.Always, Order = 3)]
        public string GameType { get; }

        /// <summary>
        ///     Gets the name of the game.
        /// </summary>
        [JsonProperty("name", Required = Required.Always, Order = 2)]
        public string Name { get; }

        /// <summary>
        ///     Gets the organizer of the game series.
        /// </summary>
        [JsonProperty("organizer", Required = Required.Always, Order = 4)]
        public IStartGameSeriesPerson Organizer { get; }

        /// <summary>
        ///     Gets the players of the game series.
        /// </summary>
        [JsonProperty("players", Required = Required.Always, Order = 5)]
        public IEnumerable<IStartGameSeriesPerson> Players { get; }
    }
}
