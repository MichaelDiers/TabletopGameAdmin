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
        ///     Creates a new instance of <see cref="Game" />.
        /// </summary>
        /// <param name="documentId">The document id of the game.</param>
        /// <param name="created">The creation time of the document.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="name">The name of the game.</param>
        /// <param name="gameTerminations">The mapping of player and termination ids.</param>
        [JsonConstructor]
        public Game(
            string? documentId,
            DateTime? created,
            string parentDocumentId,
            string name,
            IEnumerable<GameTermination> gameTerminations
        )
            : this(
                documentId,
                created,
                parentDocumentId,
                name,
                gameTerminations.Select(gt => gt as IGameTermination))
        {
        }

        /// <param name="documentId">The document id of the game.</param>
        /// <param name="created">The creation time of the document.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="name">The name of the game.</param>
        /// <param name="gameTerminations">The mapping of player and termination ids.</param>
        public Game(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            string name,
            IEnumerable<IGameTermination> gameTerminations
        )
            : base(documentId, created, parentDocumentId)
        {
            this.GameTerminations = gameTerminations;
            this.Name = name.ValidateIsNotNullOrWhitespace(nameof(name));
        }

        public Game(IGameSeries gameSeries, IGame game)
            : this(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                gameSeries.DocumentId,
                game.Name,
                game.GameTerminations)
        {
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
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(Game.NameName, this.Name);
            dictionary.Add(Game.GameTerminationsName, this.GameTerminations.Select(gt => gt.ToDictionary()));
            return dictionary;
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="Game" />.</returns>
        public new static IGame FromDictionary(IDictionary<string, object> dictionary)
        {
            var documentId = dictionary.GetString(DatabaseObject.DocumentIdName);
            var created = dictionary.GetDateTime(DatabaseObject.CreatedName);
            var parentDocumentId = dictionary.GetString(DatabaseObject.ParentDocumentIdName);

            var name = dictionary.GetString(NamedBase.NameName);
            var gameTerminations = dictionary.GetDictionaries(Game.GameTerminationsName)
                .Select(GameTermination.FromDictionary)
                .ToArray();
            return new Game(
                documentId,
                created,
                parentDocumentId,
                name,
                gameTerminations);
        }
    }
}
