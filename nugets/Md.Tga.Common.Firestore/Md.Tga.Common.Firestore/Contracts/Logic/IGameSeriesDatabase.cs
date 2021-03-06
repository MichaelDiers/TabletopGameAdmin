namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Database operations of game series collection.
    /// </summary>
    public interface IGameSeriesDatabase : IGameSeriesReadOnlyDatabase, IDatabase<IGameSeries>
    {
    }
}
