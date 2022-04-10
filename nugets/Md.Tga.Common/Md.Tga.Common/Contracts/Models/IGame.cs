namespace Md.Tga.Common.Contracts.Models
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Database;

    /// <summary>
    ///     Describes a game of a game series.
    /// </summary>
    public interface IGame : IDatabaseObject
    {
        /// <summary>
        ///     Gets the mappings of player and termination ids.
        /// </summary>
        IEnumerable<IGameTermination> GameTerminations { get; }

        /// <summary>
        ///     Gets the name of the game series.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the id of the survey.
        /// </summary>
        string SurveyDocumentId { get; }
    }
}
