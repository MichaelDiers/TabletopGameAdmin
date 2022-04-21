namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Describes a message for saving the status of a game.
    /// </summary>
    public interface ISaveGameStatusMessage : IMessage
    {
        /// <summary>
        ///     Gets the game status.
        /// </summary>
        IGameStatus GameStatus { get; }
    }
}
