namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using System.Threading.Tasks;
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Database operations of game-status collection.
    /// </summary>
    public interface IGameStatusReadOnlyDatabase : IReadOnlyDatabase<IGameStatus>
    {
        /// <summary>
        ///     Checks if a game is closed.
        /// </summary>
        /// <param name="gameDocumentId">The id of the game document.</param>
        /// <returns>A <see cref="Task" /> whose result indicates the closed status.</returns>
        Task<bool> IsClosed(string gameDocumentId);
    }
}
