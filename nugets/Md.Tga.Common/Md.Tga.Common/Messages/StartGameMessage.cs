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
        /// <param name="gameSeriesDocumentId">The document id of the game series for that a new game is started.</param>
        public StartGameMessage(string processId, string gameSeriesDocumentId)
            : base(processId)
        {
            this.GameSeriesDocumentId = gameSeriesDocumentId.ValidateIsAGuid(nameof(gameSeriesDocumentId));
        }

        /// <summary>
        ///     Gets the document id of the game series for that a new game is started.
        /// </summary>
        [JsonProperty("gameSeriesDocumentId", Required = Required.Always, Order = 11)]
        public string GameSeriesDocumentId { get; }
    }
}
