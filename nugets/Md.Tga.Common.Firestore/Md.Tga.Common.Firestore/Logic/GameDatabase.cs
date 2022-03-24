namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database for <see cref="Game" /> collections,
    /// </summary>
    public class GameDatabase : Database<Game>, IGameDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, GameReadOnlyDatabase.CollectionName, Game.FromDictionary)
        {
        }
    }
}
