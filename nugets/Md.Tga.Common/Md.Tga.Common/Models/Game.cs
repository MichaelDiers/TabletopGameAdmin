namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Database;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a game of a game series.
    /// </summary>
    public class Game : DatabaseObject, IGame
    {
        /// <summary>
        ///     The json name of <see cref="GameTerminations" />.
        /// </summary>
        public const string GameTerminationsName = "gameTerminations";

        /// <summary>
        ///     The name of json entry name.
        /// </summary>
        public const string NameName = "name";

        /// <summary>
        ///     The name of the survey id.
        /// </summary>
        public const string SurveyDocumentIdName = "surveyDocumentId";

        /// <summary>
        ///     Creates a new instance of <see cref="Game" />.
        /// </summary>
        /// <param name="documentId">The document id of the game.</param>
        /// <param name="created">The creation time of the document.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="name">The name of the game.</param>
        /// <param name="surveyDocumentId">The id of the survey.</param>
        /// <param name="gameTerminations">The mapping of player and termination ids.</param>
        [JsonConstructor]
        public Game(
            string? documentId,
            DateTime? created,
            string parentDocumentId,
            string name,
            string surveyDocumentId,
            IEnumerable<GameTermination> gameTerminations
        )
            : this(
                documentId,
                created,
                parentDocumentId,
                name,
                surveyDocumentId,
                gameTerminations.Select(gt => gt as IGameTermination))
        {
        }

        /// <param name="documentId">The document id of the game.</param>
        /// <param name="created">The creation time of the document.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="name">The name of the game.</param>
        /// <param name="surveyDocumentId">The id of the survey.</param>
        /// <param name="gameTerminations">The mapping of player and termination ids.</param>
        public Game(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            string name,
            string surveyDocumentId,
            IEnumerable<IGameTermination> gameTerminations
        )
            : base(documentId, created, parentDocumentId)
        {
            this.SurveyDocumentId = surveyDocumentId.ValidateIsAGuid(nameof(surveyDocumentId));
            this.GameTerminations = gameTerminations;
            this.Name = name.ValidateIsNotNullOrWhitespace(nameof(name));
        }

        /// <summary>
        ///     Gets the mappings of player and termination ids.
        /// </summary>
        [JsonProperty(Game.GameTerminationsName, Required = Required.Always, Order = 113)]
        public IEnumerable<IGameTermination> GameTerminations { get; }

        /// <summary>
        ///     Gets the name of the game.
        /// </summary>
        [JsonProperty(Game.NameName, Required = Required.Always, Order = 50)]
        public string Name { get; }

        /// <summary>
        ///     Gets the id of the survey.
        /// </summary>
        [JsonProperty(Game.SurveyDocumentIdName, Required = Required.Always, Order = 112)]
        public string SurveyDocumentId { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(Game.NameName, this.Name);
            dictionary.Add(Game.SurveyDocumentIdName, this.SurveyDocumentId);
            dictionary.Add(Game.GameTerminationsName, this.GameTerminations.Select(gt => gt.ToDictionary()));
            return dictionary;
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="Game" />.</returns>
        public static IGame FromDictionary(IDictionary<string, object> dictionary)
        {
            var documentId = dictionary.GetString(DatabaseObject.DocumentIdName);
            var created = dictionary.GetDateTime(DatabaseObject.CreatedName);
            var parentDocumentId = dictionary.GetString(DatabaseObject.ParentDocumentIdName);

            var name = dictionary.GetString(NamedBase.NameName);
            var surveyId = dictionary.GetString(Game.SurveyDocumentIdName);
            var gameTerminations = dictionary.GetDictionaries(Game.GameTerminationsName)
                .Select(GameTermination.FromDictionary)
                .ToArray();
            return new Game(
                documentId,
                created,
                parentDocumentId,
                name,
                surveyId,
                gameTerminations);
        }
    }
}
