namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database for <see cref="Game" /> collections,
    /// </summary>
    public class GameReadOnlyDatabase : ReadonlyDatabase<Game>, IGameReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the database collection.
        /// </summary>
        public const string CollectionName = "games";

        /// <summary>
        ///     Creates a new instance of <see cref="GameReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, GameReadOnlyDatabase.CollectionName, Game.FromDictionary)
        {
        }
    }
}
