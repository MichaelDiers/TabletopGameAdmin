namespace Md.Tga.SurveyClosedSubscriber.Contracts
{
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Read entries from the game series collection.
    /// </summary>
    public interface IGameSeriesDatabase : IReadOnlyDatabase
    {
        /// <summary>
        ///     Read a game series by id.
        /// </summary>
        /// <param name="id">The document id of the game series.</param>
        /// <returns>An <see cref="IGameSeries" />.</returns>
        Task<IGameSeries> ReadById(string id);
    }
}
