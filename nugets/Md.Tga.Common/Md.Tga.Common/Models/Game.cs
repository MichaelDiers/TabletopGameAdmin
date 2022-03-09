namespace Md.Tga.Common.Models
{
    using System.Collections.Generic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Extensions;
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
            this.InternalGameSeriesId = internalGameSeriesId.ValidateIsAGuid();
            this.SurveyId = surveyId.ValidateIsAGuid();
        }

        /// <summary>
        ///     Gets the internal game series id.
        /// </summary>
        [JsonProperty(InternalGameSeriesIdName, Required = Required.Always, Order = 111)]
        public string InternalGameSeriesId { get; }

        /// <summary>
        ///     Gets the id of the survey.
        /// </summary>
        [JsonProperty(SurveyIdName, Required = Required.Always, Order = 112)]
        public string SurveyId { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(InternalGameSeriesIdName, this.InternalGameSeriesId);
            dictionary.Add(SurveyIdName, this.SurveyId);
            return dictionary;
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="Game" />.</returns>
        public new static Game FromDictionary(IDictionary<string, object> dictionary)
        {
            var id = dictionary.GetString(IdName);
            var name = dictionary.GetString(NameName);
            var internalGameSeriesId = dictionary.GetString(InternalGameSeriesIdName);
            var surveyId = dictionary.GetString(SurveyIdName);

            return new Game(id, name, internalGameSeriesId, surveyId);
        }
    }
}
