namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Message that is sent to save a game.
    /// </summary>
    public interface ISaveGameMessage : IMessage
    {
        /// <summary>
        ///     Gets the game to be saved.
        /// </summary>
        IGame Game { get; }

        /// <summary>
        ///     The new game <see cref="Game" /> is linked to this <see cref="IGameSeries" />.
        /// </summary>
        IGameSeries GameSeries { get; }
    }
}
