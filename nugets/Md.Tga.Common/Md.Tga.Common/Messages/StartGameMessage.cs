namespace Md.Tga.Common.Messages
{
    using Md.Common.Extensions;
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
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
        /// <param name="internalGameSeriesId">The internal id of the game series.</param>
        public StartGameMessage(string processId, string internalGameSeriesId)
            : base(processId)
        {
            this.InternalGameSeriesId = internalGameSeriesId.ValidateIsAGuid(nameof(internalGameSeriesId));
        }

        /// <summary>
        ///     Gets the internal id of the game series.
        /// </summary>
        [JsonProperty("internalGameSeriesId", Required = Required.Always, Order = 11)]
        public string InternalGameSeriesId { get; }
    }
}
