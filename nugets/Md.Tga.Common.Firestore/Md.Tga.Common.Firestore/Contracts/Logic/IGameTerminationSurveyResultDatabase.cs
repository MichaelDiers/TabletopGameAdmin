namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Database operations of game termination survey result collection.
    /// </summary>
    public interface IGameTerminationSurveyResultDatabase
        : IGameTerminationSurveyResultReadOnlyDatabase, IDatabase<IGameTerminationSurveyResult>
    {
    }
}
