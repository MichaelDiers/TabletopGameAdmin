namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database for <see cref="GameName" /> collections,
    /// </summary>
    public class GameNameDatabase : Database<IGameName>, IGameNameDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameNameDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameNameDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, GameNameReadOnlyDatabase.CollectionName, GameName.FromDictionary)
        {
        }
    }
}
