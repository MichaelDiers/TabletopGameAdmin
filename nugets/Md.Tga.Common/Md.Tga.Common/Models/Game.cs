namespace Md.Tga.Common.Models
{
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a game of a game series.
    /// </summary>
    public class Game : NamedBase, IGame
    {
        /// <summary>
        ///     The name of the internal game series id.
        /// </summary>
        public const string InternalGameSeriesIdName = "internalGameSeriesId";

        /// <summary>
        ///     The name of the survey id.
        /// </summary>
        public const string SurveyIdName = "surveyId";

        /// <summary>
        ///     Creates a new instance of <see cref="Game" />.
        /// </summary>
        /// <param name="id">The id of the game.</param>
        /// <param name="name">The name of the game.</param>
        /// <param name="internalGameSeriesId">The internal game series id.</param>
        /// <param name="surveyId">The id of the survey.</param>
        public Game(
            string id,
            string name,
            string internalGameSeriesId,
            string surveyId
        )
            : base(id, name)
        {
            this.InternalGameSeriesId = internalGameSeriesId.ValidateIsAGuid(nameof(internalGameSeriesId));
            this.SurveyId = surveyId.ValidateIsAGuid(nameof(surveyId));
        }

        /// <summary>
        ///     Creates a new instance of <see cref="Game" />.
        /// </summary>
        /// <param name="id">The id of the game.</param>
        /// <param name="name">The name of the game.</param>
        /// <param name="internalGameSeriesId">The internal game series id.</param>
        /// <param name="surveyId">The id of the survey.</param>
        /// <param name="internalDocumentId">The internal document id.</param>
        protected Game(
            string id,
            string name,
            string internalGameSeriesId,
            string surveyId,
            string internalDocumentId
        )
            : base(id, name, internalDocumentId)
        {
            this.InternalGameSeriesId = internalGameSeriesId.ValidateIsAGuid(nameof(internalGameSeriesId));
            this.SurveyId = surveyId.ValidateIsAGuid(nameof(surveyId));
        }

        /// <summary>
        ///     Gets the internal game series id.
        /// </summary>
        [JsonProperty(Game.InternalGameSeriesIdName, Required = Required.Always, Order = 111)]
        public string InternalGameSeriesId { get; }

        /// <summary>
        ///     Gets the id of the survey.
        /// </summary>
        [JsonProperty(Game.SurveyIdName, Required = Required.Always, Order = 112)]
        public string SurveyId { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(Game.InternalGameSeriesIdName, this.InternalGameSeriesId);
            dictionary.Add(Game.SurveyIdName, this.SurveyId);
            return dictionary;
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="Game" />.</returns>
        public new static Game FromDictionary(IDictionary<string, object> dictionary)
        {
            var id = dictionary.GetString(Base.IdName);
            var name = dictionary.GetString(NamedBase.NameName);
            var internalGameSeriesId = dictionary.GetString(Game.InternalGameSeriesIdName);
            var surveyId = dictionary.GetString(Game.SurveyIdName);
            var internalDocumentId = dictionary.GetString(Base.InternalDocumentIdName, string.Empty);
            return new Game(
                id,
                name,
                internalGameSeriesId,
                surveyId,
                internalDocumentId);
        }
    }
}
