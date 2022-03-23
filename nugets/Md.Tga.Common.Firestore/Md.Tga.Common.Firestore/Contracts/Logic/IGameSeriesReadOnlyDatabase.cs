namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database operations of game series collection.
    /// </summary>
    public interface IGameSeriesReadOnlyDatabase : IReadOnlyDatabase<GameSeries>
    {
    }
}
