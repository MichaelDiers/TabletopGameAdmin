namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database for <see cref="GameTerminationSurvey" /> collections,
    /// </summary>
    public class GameTerminationSurveyDatabase : Database<IGameTerminationSurvey>, IGameTerminationSurveyDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationSurveyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameTerminationSurveyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(
                runtimeEnvironment,
                GameTerminationSurveyReadOnlyDatabase.CollectionName,
                GameTerminationSurvey.FromDictionary)
        {
        }
    }
}
