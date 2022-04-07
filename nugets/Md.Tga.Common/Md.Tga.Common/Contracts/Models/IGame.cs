namespace Md.Tga.Common.Contracts.Models
{
    using System.Collections.Generic;

    /// <summary>
    ///     Describes a game of a game series.
    /// </summary>
    public interface IGame : INamedBase
    {
        /// <summary>
        ///     Gets the mappings of player and termination ids.
        /// </summary>
        IEnumerable<IGameTermination> GameTerminations { get; }

        /// <summary>
        ///     Gets the internal game series id.
        /// </summary>
        string InternalGameSeriesId { get; }

        /// <summary>
        ///     Gets the id of the survey.
        /// </summary>
        string SurveyId { get; }
    }
}
