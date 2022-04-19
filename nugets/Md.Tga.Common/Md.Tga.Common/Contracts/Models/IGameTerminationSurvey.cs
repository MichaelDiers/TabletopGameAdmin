namespace Md.Tga.Common.Contracts.Models
{
    using Md.Common.Contracts.Database;

    /// <summary>
    ///     Describes a game termination survey.
    /// </summary>
    public interface IGameTerminationSurvey : IDatabaseObject
    {
        /// <summary>
        ///     Gets the id of the winning side.
        /// </summary>
        string WinningSideId { get; }
    }
}
