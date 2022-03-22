namespace Md.Tga.SurveyClosedSubscriber.Contracts
{
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Read entries from the games collection.
    /// </summary>
    public interface IGamesDatabase : IReadOnlyDatabase
    {
        /// <summary>
        ///     Read a game by survey id.
        /// </summary>
        /// <param name="surveyId">The survey id that is associated with the game.</param>
        /// <returns>An <see cref="IGame" />.</returns>
        Task<IGame> ReadGameBySurveyId(string surveyId);
    }
}
