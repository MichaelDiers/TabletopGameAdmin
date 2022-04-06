namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database for <see cref="PlayerMappings" /> collections.
    /// </summary>
    public class PlayerMappingsDatabase : Database<IPlayerMappings>, IPlayerMappingsDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="PlayerMappingsDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public PlayerMappingsDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, PlayerMappingsReadOnlyDatabase.CollectionName, PlayerMappings.FromDictionary)
        {
        }
    }
}
