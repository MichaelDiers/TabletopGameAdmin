namespace Md.Tga.Common.Messages
{
    using System;
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a message that describes a new game series.
    /// </summary>
    public class InitializeGameSeriesMessage : Message, IInitializeGameSeriesMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="InitializeGameSeriesMessage" />.
        /// </summary>
        /// <param name="processId">The id of the process.</param>
        /// <param name="gameSeries">The data of the new game series.</param>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="gameSeries" /> is null.</exception>
        public InitializeGameSeriesMessage(string processId, IGameSeries gameSeries)
            : base(processId)
        {
            this.GameSeries = gameSeries ?? throw new ArgumentNullException(nameof(gameSeries));
        }

        /// <summary>
        ///     Creates a new instance of <see cref="InitializeGameSeriesMessage" />.
        /// </summary>
        /// <param name="processId">The id of the process.</param>
        /// <param name="gameSeries">The data of the new game series.</param>
        /// <exception cref="ArgumentNullException">Is thrown if <paramref name="gameSeries" /> is null.</exception>
        [JsonConstructor]
        public InitializeGameSeriesMessage(string processId, GameSeries gameSeries)
            : this(processId, gameSeries as IGameSeries)
        {
        }

        /// <summary>
        ///     Get the data of the game series.
        /// </summary>
        [JsonProperty("gameSeries", Required = Required.Always, Order = 11)]
        public IGameSeries GameSeries { get; }
    }
}
