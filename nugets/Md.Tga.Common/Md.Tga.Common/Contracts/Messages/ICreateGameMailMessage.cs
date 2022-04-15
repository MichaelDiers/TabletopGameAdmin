namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Message that is sent for creating an email.
    /// </summary>
    public interface ICreateGameMailMessage : IMessage
    {
        /// <summary>
        ///     Gets the game to be saved.
        /// </summary>
        IGame Game { get; }

        /// <summary>
        ///     The new game <see cref="Game" /> is linked to this <see cref="IGameSeries" />.
        /// </summary>
        IGameSeries GameSeries { get; }

        /// <summary>
        ///     Gets the player mappings for the game.
        /// </summary>
        IPlayerMappings PlayerMappings { get; }
    }
}
