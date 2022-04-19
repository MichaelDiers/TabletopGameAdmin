namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database for <see cref="GameTerminationSurveyResult" /> collections,
    /// </summary>
    public class GameTerminationSurveyResultDatabase
        : Database<IGameTerminationSurveyResult>, IGameTerminationSurveyResultDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationSurveyResultDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameTerminationSurveyResultDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(
                runtimeEnvironment,
                GameTerminationSurveyResultReadOnlyDatabase.CollectionName,
                GameTerminationSurveyResult.FromDictionary)
        {
        }
    }
}
