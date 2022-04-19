namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database access for <see cref="GameTerminationSurveyResult" /> collections,
    /// </summary>
    public class GameTerminationSurveyResultReadOnlyDatabase
        : ReadonlyDatabase<IGameTerminationSurveyResult>, IGameTerminationSurveyResultReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the database collection.
        /// </summary>
        public const string CollectionName = "game-termination-survey-result";

        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationSurveyResultReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameTerminationSurveyResultReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(
                runtimeEnvironment,
                GameTerminationSurveyResultReadOnlyDatabase.CollectionName,
                GameTerminationSurveyResult.FromDictionary)
        {
        }
    }
}
