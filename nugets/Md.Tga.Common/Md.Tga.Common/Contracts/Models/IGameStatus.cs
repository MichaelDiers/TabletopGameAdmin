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
    }
}
