namespace Md.Tga.StartGameSubscriber.Logic
{
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.StartGameSubscriber.Contracts;

    /// <summary>
    ///     Access to the database collection game-series.
    /// </summary>
    public class GameSeriesReadOnlyDatabase : ReadonlyDatabase, IGameSeriesReadOnlyDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameSeriesReadOnlyDatabase" />.
        /// </summary>
        /// <param name="databaseConfiguration">The database configuration.</param>
        public GameSeriesReadOnlyDatabase(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }
    }
}
