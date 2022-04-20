namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Start the evaluation of a game.
    /// </summary>
    public interface IEvaluateGameTerminationMessage : IMessage
    {
        /// <summary>
        ///     Gets the id of the game document.
        /// </summary>
        string GameDocumentId { get; }
    }
}
