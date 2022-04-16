namespace Md.Tga.Common.Contracts.Models
{
    using Md.Common.Contracts.Database;

    /// <summary>
    ///     Describes a game name entity.
    /// </summary>
    public interface IGameName : IDatabaseObject
    {
        /// <summary>
        ///     Gets the name of the game.
        /// </summary>
        string Name { get; }
    }
}
