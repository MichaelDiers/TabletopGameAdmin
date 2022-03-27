namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database for <see cref="GameSeries" /> collections,
    /// </summary>
    public class GameSeriesReadOnlyDatabase : ReadonlyDatabase<IGameSeries>, IGameSeriesReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the database collection.
        /// </summary>
        public const string CollectionName = "game-series";

        /// <summary>
        ///     Creates a new instance of <see cref="GameSeriesReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameSeriesReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, GameSeriesReadOnlyDatabase.CollectionName, GameSeries.FromDictionary)
        {
        }
    }
}
