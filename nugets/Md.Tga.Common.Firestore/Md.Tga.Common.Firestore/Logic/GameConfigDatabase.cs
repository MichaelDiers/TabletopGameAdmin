namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database for <see cref="GameConfig" /> collections,
    /// </summary>
    public class GameConfigDatabase : Database<IGameConfig>, IGameConfigDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameConfigDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, GameConfigReadOnlyDatabase.CollectionName, GameConfig.FromDictionary)
        {
        }
    }
}
