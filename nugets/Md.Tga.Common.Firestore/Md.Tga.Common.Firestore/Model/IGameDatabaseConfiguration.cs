namespace Md.Tga.Common.Firestore.Model
{
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Firestore.Contracts.Model;

    /// <summary>
    ///     Game database configuration.
    /// </summary>
    public class GameDatabaseConfiguration : DatabaseConfiguration, IGameDatabaseConfiguration
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameDatabaseConfiguration" />.
        /// </summary>
        /// <param name="projectId">The id of the project.</param>
        /// <param name="collectionName">The name of the collection.</param>
        public GameDatabaseConfiguration(string projectId, string collectionName)
            : base(projectId, collectionName)
        {
        }
    }
}
