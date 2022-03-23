namespace Md.Tga.Common.Firestore.Model
{
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Firestore.Contracts.Model;

    /// <summary>
    ///     Game series database configuration.
    /// </summary>
    public class GameSeriesDatabaseConfiguration : DatabaseConfiguration, IGameSeriesDatabaseConfiguration
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameSeriesDatabaseConfiguration" />.
        /// </summary>
        /// <param name="projectId">The id of the project.</param>
        /// <param name="collectionName">The name of the collection.</param>
        public GameSeriesDatabaseConfiguration(string projectId, string collectionName)
            : base(projectId, collectionName)
        {
        }
    }
}
