namespace Md.Tga.Common.Contracts.Models
{
    using Md.Common.Contracts.Database;

    /// <summary>
    ///     Describes the status of a <see cref="IGame" />.
    /// </summary>
    public interface IGameStatus : IDatabaseObject
    {
        /// <summary>
        ///     Gets the status.
        /// </summary>
        Status Status { get; }

        /// <summary>
        /// Gets the id of winning side if the status is <see cref="Status.Closed"/>.
        /// </summary>
        string WinningSideId { get; }
    }
}
