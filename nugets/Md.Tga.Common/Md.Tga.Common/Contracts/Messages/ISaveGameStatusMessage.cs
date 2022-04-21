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
        ///     Gets an optional message that is created by the receiver of the <see cref="ISaveGameStatusMessage" />.
        /// </summary>
        ICreateGameMailMessage? CreateGameMailMessage { get; }

        /// <summary>
        ///     Gets the game status.
        /// </summary>
        IGameStatus GameStatus { get; }
    }
}
