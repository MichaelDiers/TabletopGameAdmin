namespace Md.Tga.Common.Messages
{
    using System;
    using Md.Common.Extensions;
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
            this.GameSeries = gameSeries ?? throw new ArgumentNullException(nameof(gameSeries));
            this.InternalId = internalId.ValidateIsAGuid(nameof(internalId));
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
        ///     Get the data of the game series.
        /// </summary>
        [JsonProperty("gameSeries", Required = Required.Always, Order = 11)]
        public IGameSeries GameSeries { get; }
    }
}
