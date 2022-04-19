namespace Md.Tga.Common.Contracts.Models
{
    /// <summary>
    ///     Describes a game termination result.
    /// </summary>
    public interface IGameTerminationSurveyResult
    {
        /// <summary>
        ///     Gets a value that indicates if the player accepts the termination.
        /// </summary>
        bool Accept { get; }

        /// <summary>
        ///     Gets the id of the player.
        /// </summary>
        string PlayerId { get; }
    }
}
