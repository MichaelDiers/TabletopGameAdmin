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
    ///     Database for <see cref="GameStatus" /> collections,
    /// </summary>
    public class GameStatusDatabase : Database<IGameStatus>, IGameStatusDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameStatusDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameStatusDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, GameStatusReadOnlyDatabase.CollectionName, GameStatus.FromDictionary)
        {
        }

        /// <summary>
        ///     Checks if a game is closed.
        /// </summary>
        /// <param name="gameDocumentId">The id of the game document.</param>
        /// <returns>A <see cref="Task" /> whose result indicates the closed status.</returns>
        public async Task<bool> IsClosed(string gameDocumentId)
        {
            var snapshot = await this.Collection()
                .WhereEqualTo(DatabaseObject.ParentDocumentIdName, gameDocumentId)
                .WhereEqualTo(GameStatus.StatusName, Status.Closed)
                .Limit(1)
                .GetSnapshotAsync();
            return snapshot is {Count: 1};
        }
    }
}
