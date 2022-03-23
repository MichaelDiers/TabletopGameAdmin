namespace Md.Tga.Common.Firestore.Logic
{
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database for <see cref="Game" /> collections,
    /// </summary>
    public class GameReadOnlyDatabase : ReadonlyDatabase<Game>, IGameReadOnlyDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameReadOnlyDatabase" />.
        /// </summary>
        /// <param name="configuration">The database configuration.</param>
        public GameReadOnlyDatabase(IDatabaseConfiguration configuration)
            : base(configuration, Game.FromDictionary)
        {
        }
    }
}
