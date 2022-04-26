namespace Md.Tga.Common.Messages
{
    using Md.Common.Extensions;
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Newtonsoft.Json;

    /// <summary>
    ///     Start the termination of a game.
    /// </summary>
    public class StartGameTerminationMessage : Message, IStartGameTerminationMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="StartGameTerminationMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeriesDocumentId">The document id of the game series.</param>
        /// <param name="gameDocumentId">The document id of the game.</param>
        /// <param name="terminationId">The termination id.</param>
        /// <param name="winningSideId">The id of the winning side.</param>
        /// <param name="reason">A reason for terminating the game.</param>
        public StartGameTerminationMessage(
            string processId,
            string gameSeriesDocumentId,
            string gameDocumentId,
            string terminationId,
            string winningSideId,
            string reason
        )
            : base(processId)
        {
            this.GameSeriesDocumentId = gameSeriesDocumentId.ValidateIsAGuid(nameof(gameSeriesDocumentId));
            this.GameDocumentId = gameDocumentId.ValidateIsAGuid(nameof(gameDocumentId));
            this.TerminationId = terminationId.ValidateIsAGuid(nameof(terminationId));
            this.WinningSideId = winningSideId.ValidateIsAGuid(nameof(winningSideId));
            this.Reason = reason;
        }

        /// <summary>
        ///     Gets the id of the game document.
        /// </summary>
        [JsonProperty("gameId", Required = Required.Always, Order = 12)]
        public string GameDocumentId { get; }

        /// <summary>
        ///     Gets the id of the game series document.
        /// </summary>
        [JsonProperty("gameSeriesId", Required = Required.Always, Order = 11)]
        public string GameSeriesDocumentId { get; }

        /// <summary>
        ///     Gets a reason for the termination of a game.
        /// </summary>
        [JsonProperty("reason", Required = Required.Always, Order = 15)]
        public string Reason { get; }

        /// <summary>
        ///     Gets the termination id.
        /// </summary>
        [JsonProperty("terminationId", Required = Required.Always, Order = 13)]
        public string TerminationId { get; }

        /// <summary>
        ///     Gets the id of the winning side.
        /// </summary>
        [JsonProperty("winningSideId", Required = Required.Always, Order = 14)]
        public string WinningSideId { get; }
    }
}
