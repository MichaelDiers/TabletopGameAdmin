namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database for <see cref="GameName" /> collections,
    /// </summary>
    public class GameNameReadOnlyDatabase : ReadonlyDatabase<IGameName>, IGameNameReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the database collection.
        /// </summary>
        public const string CollectionName = "game-names";

        /// <summary>
        ///     Creates a new instance of <see cref="GameNameReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameNameReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, GameNameReadOnlyDatabase.CollectionName, GameName.FromDictionary)
        {
        }
    }
}
