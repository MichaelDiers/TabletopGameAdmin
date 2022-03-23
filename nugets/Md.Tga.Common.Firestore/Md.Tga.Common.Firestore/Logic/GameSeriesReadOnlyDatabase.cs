namespace Md.Tga.Common.Firestore.Logic
{
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Firestore.Contracts.Model;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database for <see cref="GameSeries" /> collections,
    /// </summary>
    public class GameSeriesReadOnlyDatabase : ReadonlyDatabase<GameSeries>, IGameSeriesReadOnlyDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameSeriesReadOnlyDatabase" />.
        /// </summary>
        /// <param name="configuration">The database configuration.</param>
        public GameSeriesReadOnlyDatabase(IGameSeriesDatabaseConfiguration configuration)
            : base(configuration, GameSeries.FromDictionary)
        {
        }
    }
}
