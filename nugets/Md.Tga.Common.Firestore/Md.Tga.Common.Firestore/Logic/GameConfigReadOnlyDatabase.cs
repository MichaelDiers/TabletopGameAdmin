namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database for <see cref="GameConfig" /> collections,
    /// </summary>
    public class GameConfigReadOnlyDatabase : ReadonlyDatabase<IGameConfig>, IGameConfigReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the database collection.
        /// </summary>
        public const string CollectionName = "game-configs";

        /// <summary>
        ///     Creates a new instance of <see cref="GameReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameConfigReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, GameConfigReadOnlyDatabase.CollectionName, GameConfig.FromDictionary)
        {
        }
    }
}
