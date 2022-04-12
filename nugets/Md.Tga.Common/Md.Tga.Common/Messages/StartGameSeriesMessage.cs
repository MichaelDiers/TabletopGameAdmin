namespace Md.Tga.Common.Messages
{
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a start game series message.
    /// </summary>
    public class StartGameSeriesMessage : Message, IStartGameSeriesMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="StartGameSeriesMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The game series data.</param>
        public StartGameSeriesMessage(string processId, IStartGameSeries gameSeries)
            : base(processId)
        {
            this.GameSeries = gameSeries;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="StartGameSeriesMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The game series data.</param>
        [JsonConstructor]
        public StartGameSeriesMessage(string processId, StartGameSeries gameSeries)
            : this(processId, gameSeries as IStartGameSeries)
        {
        }

        /// <summary>
        ///     Gets the game series data.
        /// </summary>
        [JsonProperty("gameSeries", Required = Required.Always, Order = 11)]
        public IStartGameSeries GameSeries { get; }
    }
}
