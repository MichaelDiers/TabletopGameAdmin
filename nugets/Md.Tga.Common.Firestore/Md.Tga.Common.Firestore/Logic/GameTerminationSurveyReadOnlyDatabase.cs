namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database access for <see cref="GameTerminationSurvey" /> collections,
    /// </summary>
    public class GameTerminationSurveyReadOnlyDatabase
        : ReadonlyDatabase<IGameTerminationSurvey>, IGameTerminationSurveyReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the database collection.
        /// </summary>
        public const string CollectionName = "game-termination-survey";

        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationSurveyReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameTerminationSurveyReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(
                runtimeEnvironment,
                GameTerminationSurveyReadOnlyDatabase.CollectionName,
                GameTerminationSurvey.FromDictionary)
        {
        }
    }
}
