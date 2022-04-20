namespace Md.Tga.Common.Contracts.Models
{
    using Md.Common.Contracts.Database;

    /// <summary>
    ///     Describes a game termination result.
    /// </summary>
    public interface IGameTerminationResult : IDatabaseObject
    {
        /// <summary>
        ///     Gets the id of the player.
        /// </summary>
        string PlayerId { get; }

        /// <summary>
        ///     Gets the id of the winning side.
        /// </summary>
        string WinningSideId { get; }
    }
}
