namespace Md.Tga.Common.Messages
{
    using Md.Common.Extensions;
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;

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
        public StartGameTerminationMessage(
            string processId,
            string gameSeriesDocumentId,
            string gameDocumentId,
            string terminationId,
            string winningSideId
        )
            : base(processId)
        {
            this.GameSeriesDocumentId = gameSeriesDocumentId.ValidateIsAGuid(nameof(gameSeriesDocumentId));
            this.GameDocumentId = gameDocumentId.ValidateIsAGuid(nameof(gameDocumentId));
            this.TerminationId = terminationId.ValidateIsAGuid(nameof(terminationId));
            this.WinningSideId = winningSideId.ValidateIsAGuid(nameof(winningSideId));
        }

        /// <summary>
        ///     Gets the id of the game document.
        /// </summary>
        public string GameDocumentId { get; }

        /// <summary>
        ///     Gets the id of the game series document.
        /// </summary>
        public string GameSeriesDocumentId { get; }

        /// <summary>
        ///     Gets the termination id.
        /// </summary>
        public string TerminationId { get; }

        /// <summary>
        ///     Gets the id of the winning side.
        /// </summary>
        public string WinningSideId { get; }
    }
}
