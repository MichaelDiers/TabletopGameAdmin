namespace Md.Tga.Common.Firestore.Logic
{
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Firestore.Contracts.Model;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database for <see cref="Game" /> collections,
    /// </summary>
    public class GameDatabase : Database<Game>, IGameDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameDatabase" />.
        /// </summary>
        /// <param name="configuration">The database configuration.</param>
        public GameDatabase(IGameDatabaseConfiguration configuration)
            : base(configuration, Game.FromDictionary)
        {
        }
    }
}
