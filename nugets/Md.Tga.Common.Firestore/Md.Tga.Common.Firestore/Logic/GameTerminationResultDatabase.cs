namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database for <see cref="GameTerminationResult" /> collections,
    /// </summary>
    public class GameTerminationResultDatabase : Database<IGameTerminationResult>, IGameTerminationResultDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationResultDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameTerminationResultDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(
                runtimeEnvironment,
                GameTerminationResultReadOnlyDatabase.CollectionName,
                GameTerminationResult.FromDictionary)
        {
        }
    }
}
