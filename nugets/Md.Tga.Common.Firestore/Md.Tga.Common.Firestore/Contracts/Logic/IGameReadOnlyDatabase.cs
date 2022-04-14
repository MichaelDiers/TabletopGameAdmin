namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using System.Threading.Tasks;
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Database operations of games collection.
    /// </summary>
    public interface IGameReadOnlyDatabase : IReadOnlyDatabase<IGame>
    {
        /// <summary>
        ///     Count the number of games that a  part of a game series.
        /// </summary>
        /// <param name="gameSeriesDocumentId">The document id of the game series.</param>
        /// <returns>The document count.</returns>
        Task<int> CountGames(string gameSeriesDocumentId);
    }
}
