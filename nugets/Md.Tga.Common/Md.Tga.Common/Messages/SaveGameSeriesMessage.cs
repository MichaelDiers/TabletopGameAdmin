namespace Md.Tga.Common.Messages
{
    using System;
    using Md.GoogleCloud.Base.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a message that describes a new game series.
    /// </summary>
    public class SaveGameSeriesMessage : Message, ISaveGameSeriesMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="InitializeGameSeriesMessage" />.
        /// </summary>
        /// <param name="processId">The id of the process.</param>
        /// <param name="gameSeries">The data of the new game series.</param>
        /// <param name="internalId">The internal id.</param>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="gameSeries" /> is null.</exception>
        public SaveGameSeriesMessage(string processId, IGameSeries gameSeries, string internalId)
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

            this.GameSeries = gameSeries ?? throw new ArgumentNullException(nameof(gameSeries));
            this.InternalId = internalId;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="InitializeGameSeriesMessage" />.
        /// </summary>
        /// <param name="processId">The id of the process.</param>
        /// <param name="gameSeries">The data of the new game series.</param>
        /// <param name="internalId">The internal id.</param>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="gameSeries" /> is null.</exception>
        [JsonConstructor]
        public SaveGameSeriesMessage(string processId, GameSeries gameSeries, string internalId)
            : this(processId, gameSeries as IGameSeries, internalId)
        {
        }

        /// <summary>
        ///     Get the data the internal id.
        /// </summary>
        [JsonProperty("internalId", Required = Required.Always, Order = 12)]
        public string InternalId { get; }

        /// <summary>
        ///     Get the data of the game series.
        /// </summary>
        [JsonProperty("gameSeries", Required = Required.Always, Order = 11)]
        public IGameSeries GameSeries { get; }
    }
}
