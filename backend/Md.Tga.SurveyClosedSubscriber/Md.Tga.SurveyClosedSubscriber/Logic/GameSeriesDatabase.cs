namespace Md.Tga.SurveyClosedSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Md.Tga.SurveyClosedSubscriber.Contracts;

    /// <summary>
    ///     Read entries from the game series collection.
    /// </summary>
    public class GameSeriesDatabase : ReadonlyDatabase, IGameSeriesDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameSeriesDatabase" />.
        /// </summary>
        /// <param name="databaseConfiguration"></param>
        public GameSeriesDatabase(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }

        /// <summary>
        ///     Read a game series by id.
        /// </summary>
        /// <param name="id">The document id of the game series.</param>
        /// <returns>An <see cref="IGameSeries" />.</returns>
        public async Task<IGameSeries> ReadById(string id)
        {
            var gameSeries = await this.ReadByDocumentIdAsync(id);
            if (gameSeries != null)
            {
                return GameSeries.FromDictionary(gameSeries);
            }

            throw new ArgumentException($"Unknown game series id {id}");
        }
    }
}
