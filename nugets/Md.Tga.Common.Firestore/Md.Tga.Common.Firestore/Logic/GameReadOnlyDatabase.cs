namespace Md.Tga.Common.Firestore.Logic
{
    using System.Threading.Tasks;
    using Md.Common.Contracts.Model;
    using Md.Common.Database;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database for <see cref="Game" /> collections,
    /// </summary>
    public class GameReadOnlyDatabase : ReadonlyDatabase<IGame>, IGameReadOnlyDatabase
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

        /// <summary>
        ///     Count the number of games that a  part of a game series.
        /// </summary>
        /// <param name="gameSeriesDocumentId">The document id of the game series.</param>
        /// <returns>The document count.</returns>
        public async Task<int> CountGames(string gameSeriesDocumentId)
        {
            var snapshot = await this.Collection()
                .WhereEqualTo(DatabaseObject.ParentDocumentIdName, gameSeriesDocumentId)
                .GetSnapshotAsync();
            return snapshot.Count;
        }
    }
}
