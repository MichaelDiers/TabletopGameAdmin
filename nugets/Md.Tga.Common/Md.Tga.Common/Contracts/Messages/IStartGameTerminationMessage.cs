namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Start the termination of a game.
    /// </summary>
    public interface IStartGameTerminationMessage : IMessage
    {
        /// <summary>
        ///     Gets the id of the game document.
        /// </summary>
        string GameDocumentId { get; }

        /// <summary>
        ///     Gets the id of the game series document.
        /// </summary>
        string GameSeriesDocumentId { get; }

        /// <summary>
        ///     Gets a reason for the termination of a game.
        /// </summary>
        string Reason { get; }

        /// <summary>
        ///     Gets the termination id.
        /// </summary>
        string TerminationId { get; }

        /// <summary>
        ///     Gets the id of the winning side.
        /// </summary>
        string WinningSideId { get; }
    }
}
