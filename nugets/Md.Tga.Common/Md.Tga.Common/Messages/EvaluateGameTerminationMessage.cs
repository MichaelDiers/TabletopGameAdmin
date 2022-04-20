namespace Md.Tga.Common.Messages
{
    using Md.Common.Extensions;
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Newtonsoft.Json;

    /// <summary>
    ///     Start the evaluation of a game.
    /// </summary>
    public class EvaluateGameTerminationMessage : Message, IEvaluateGameTerminationMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="EvaluateGameTerminationMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameDocumentId">The document id of the game.</param>
        public EvaluateGameTerminationMessage(string processId, string gameDocumentId)
            : base(processId)
        {
            this.GameDocumentId = gameDocumentId.ValidateIsAGuid(nameof(gameDocumentId));
        }

        /// <summary>
        ///     Gets the id of the game document.
        /// </summary>
        [JsonProperty("gameId", Required = Required.Always, Order = 11)]
        public string GameDocumentId { get; }
    }
}
