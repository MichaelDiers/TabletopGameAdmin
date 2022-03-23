namespace Md.Tga.Common.Firestore.Logic
{
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Firestore.Contracts.Model;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database for <see cref="GameSeries" /> collections,
    /// </summary>
    public class GameSeriesDatabase : Database<GameSeries>, IGameSeriesDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameSeriesDatabase" />.
        /// </summary>
        /// <param name="configuration">The database configuration.</param>
        public GameSeriesDatabase(IGameSeriesDatabaseConfiguration configuration)
            : base(configuration, GameSeries.FromDictionary)
        {
        }
    }
}
