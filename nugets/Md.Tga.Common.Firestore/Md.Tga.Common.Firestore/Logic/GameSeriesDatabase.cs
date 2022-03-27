namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database for <see cref="GameSeries" /> collections,
    /// </summary>
    public class GameSeriesDatabase : Database<IGameSeries>, IGameSeriesDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameSeriesDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameSeriesDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, GameSeriesReadOnlyDatabase.CollectionName, GameSeries.FromDictionary)
        {
        }
    }
}
