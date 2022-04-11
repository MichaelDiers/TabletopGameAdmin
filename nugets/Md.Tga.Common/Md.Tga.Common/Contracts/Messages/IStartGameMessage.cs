namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Describes a start game message.
    /// </summary>
    public interface IStartGameMessage : IMessage
    {
        /// <summary>
        ///     Gets the document id of the game series for that a new game is started.
        /// </summary>
        string GameSeriesDocumentId { get; }
    }
}
