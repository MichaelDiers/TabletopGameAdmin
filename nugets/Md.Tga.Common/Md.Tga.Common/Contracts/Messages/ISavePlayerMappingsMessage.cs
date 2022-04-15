namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Describes a message for saving <see cref="IPlayerMappings" />.
    /// </summary>
    public interface ISavePlayerMappingsMessage : IMessage
    {
        /// <summary>
        ///     Gets the game.
        /// </summary>
        IGame Game { get; }

        /// <summary>
        ///     Gets the game series.
        /// </summary>
        IGameSeries GameSeries { get; }

        /// <summary>
        ///     Gets the player mappings.
        /// </summary>
        IPlayerMappings PlayerMappings { get; }
    }
}
