namespace Md.TabletopGameAdmin.Common.Messages
{
    using System;
    using Md.GoogleCloud.Base.Messages;
    using Md.TabletopGameAdmin.Common.Contracts.Messages;
    using Md.TabletopGameAdmin.Common.Contracts.Models;
    using Md.TabletopGameAdmin.Common.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a start game message.
    /// </summary>
    public class StartGameMessage : Message, IStartGameMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="StartGameMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="internalId">The internal id of the game series.</param>
        [JsonConstructor]
        public StartGameMessage(string processId, string internalId)
            : this(processId, null, internalId)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="StartGameMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The data of the game series.</param>
        /// <param name="internalId">The internal id of the game series.</param>
        [JsonConstructor]
        public StartGameMessage(string processId, GameSeries? gameSeries, string internalId)
            : this(processId, gameSeries as IGameSeries, internalId)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="StartGameMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The data of the game series.</param>
        /// <param name="internalId">The internal id of the game series.</param>
        public StartGameMessage(string processId, IGameSeries? gameSeries, string internalId)
            : base(processId)
        {
            if (string.IsNullOrWhiteSpace(internalId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(internalId));
            }

            if (!Guid.TryParse(internalId, out var guid) || guid == Guid.Empty)
            {
                throw new ArgumentException("Value is not a valid guid.", nameof(internalId));
            }

            this.GameSeries = gameSeries;
            this.InternalId = internalId;
        }

        /// <summary>
        ///     Gets the game series data. The data is optional.
        /// </summary>
        [JsonProperty("gameSeries", Order = 12)]
        public IGameSeries? GameSeries { get; }

        /// <summary>
        ///     Gets the internal id of the game series.
        /// </summary>
        [JsonProperty("internalId", Required = Required.Always, Order = 11)]
        public string InternalId { get; }
    }
}
