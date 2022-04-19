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
        /// <param name="gameDocumentId">The document id of the game.</param>
        /// <param name="terminationId">The termination id.</param>
        /// <param name="surrender">Indicates if a player surrenders or claims the victory.</param>
        public StartGameTerminationMessage(
            string processId,
            string gameDocumentId,
            string terminationId,
            bool surrender
        )
            : base(processId)
        {
            this.GameDocumentId = gameDocumentId.ValidateIsAGuid(nameof(gameDocumentId));
            this.TerminationId = terminationId.ValidateIsAGuid(nameof(terminationId));
            this.Surrender = surrender;
        }

        /// <summary>
        ///     Gets the id of the document.
        /// </summary>
        public string GameDocumentId { get; }

        /// <summary>
        ///     Gets a value that indicates if a player surrenders or claims the victory.
        /// </summary>
        public bool Surrender { get; }

        /// <summary>
        ///     Gets the termination id.
        /// </summary>
        public string TerminationId { get; }
    }
}
