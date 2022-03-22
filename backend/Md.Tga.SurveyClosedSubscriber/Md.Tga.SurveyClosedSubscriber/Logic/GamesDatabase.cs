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
    ///     Read entries from the games collection.
    /// </summary>
    public class GamesDatabase : ReadonlyDatabase, IGamesDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GamesDatabase" />.
        /// </summary>
        /// <param name="databaseConfiguration"></param>
        public GamesDatabase(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }

        /// <summary>
        ///     Read a game by survey id.
        /// </summary>
        /// <param name="surveyId">The survey id that is associated with the game.</param>
        /// <returns>An <see cref="IGame" />.</returns>
        public async Task<IGame> ReadGameBySurveyId(string surveyId)
        {
            var game = await this.ReadOneAsync(Game.SurveyIdName, surveyId);
            if (game != null)
            {
                return Game.FromDictionary(game);
            }

            throw new ArgumentException($"Unknown survey id {surveyId}");
        }
    }
}
