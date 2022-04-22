namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using System.Threading.Tasks;
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Database operations of game-status collection.
    /// </summary>
    public interface IGameStatusDatabase : IGameStatusReadOnlyDatabase, IDatabase<IGameStatus>
    {
        Task<string?> InsertIfNotExistsAsync(IGameStatus gameStatus);
    }
}
