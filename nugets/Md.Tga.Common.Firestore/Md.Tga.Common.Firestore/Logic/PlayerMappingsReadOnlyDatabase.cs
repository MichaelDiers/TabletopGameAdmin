namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database on the <see cref="PlayerMappings" /> collection.
    /// </summary>
    public class PlayerMappingsReadOnlyDatabase : ReadonlyDatabase<IPlayerMappings>, IPlayerMappingsReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the database collection.
        /// </summary>
        public const string CollectionName = "player-mappings";

        /// <summary>
        ///     Creates a new instance of <see cref="GameReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public PlayerMappingsReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, PlayerMappingsReadOnlyDatabase.CollectionName, PlayerMappings.FromDictionary)
        {
        }
    }
}
