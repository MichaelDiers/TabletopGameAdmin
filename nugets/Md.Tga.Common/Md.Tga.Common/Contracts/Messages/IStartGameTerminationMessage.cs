namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Start the termination of a game.
    /// </summary>
    public interface IStartGameTerminationMessage : IMessage
    {
        /// <summary>
        ///     Gets the id of the document.
        /// </summary>
        string GameDocumentId { get; }

        /// <summary>
        ///     Gets a value that indicates if a player surrenders or claims the victory.
        /// </summary>
        bool Surrender { get; }

        /// <summary>
        ///     Gets the termination id.
        /// </summary>
        string TerminationId { get; }
    }
}
