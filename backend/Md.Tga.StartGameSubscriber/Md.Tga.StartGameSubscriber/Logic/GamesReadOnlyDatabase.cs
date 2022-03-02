namespace Md.Tga.StartGameSubscriber.Logic
{
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.StartGameSubscriber.Contracts;

    /// <summary>
    ///     Access to the database collection games.
    /// </summary>
    public class GamesReadOnlyDatabase : ReadonlyDatabase, IGamesReadOnlyDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GamesReadOnlyDatabase" />.
        /// </summary>
        /// <param name="databaseConfiguration">The database configuration.</param>
        public GamesReadOnlyDatabase(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }
    }
}
