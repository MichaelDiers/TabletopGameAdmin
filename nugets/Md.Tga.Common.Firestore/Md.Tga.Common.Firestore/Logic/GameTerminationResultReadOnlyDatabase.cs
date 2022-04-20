namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database access for <see cref="GameTerminationResult" /> collections,
    /// </summary>
    public class GameTerminationResultReadOnlyDatabase
        : ReadonlyDatabase<IGameTerminationResult>, IGameTerminationResultReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the database collection.
        /// </summary>
        public const string CollectionName = "game-termination-result";

        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationResultReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameTerminationResultReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(
                runtimeEnvironment,
                GameTerminationResultReadOnlyDatabase.CollectionName,
                GameTerminationResult.FromDictionary)
        {
        }
    }
}
