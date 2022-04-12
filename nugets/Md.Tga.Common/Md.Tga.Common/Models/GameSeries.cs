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
    ///     Describes a game series.
    /// </summary>
    public class GameSeries : DatabaseObject, IGameSeries

    {
        /// <summary>
        ///     The name of json entry countries.
        /// </summary>
        public const string CountriesName = "countries";

        /// <summary>
        ///     The name of json entry <see cref="ExternalId" />.
        /// </summary>
        public const string ExternalIdName = "externalId";


        /// <summary>
        ///     The name if the json entry gameType.
        /// </summary>
        public const string GameTypeName = "gameType";

        /// <summary>
        ///     The name of json entry name.
        /// </summary>
        public const string NameName = "name";

        /// <summary>
        ///     The name of json entry organizer.
        /// </summary>
        public const string OrganizerName = "organizer";

        /// <summary>
        ///     The name of json entry players.
        /// </summary>
        public const string PlayersName = "players";

        /// <summary>
        ///     The name of json entry sides.
        /// </summary>
        public const string SidesName = "sides";

        /// <summary>
        ///     Create a new instance of <see cref="GameSeries" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the document.</param>
        /// <param name="externalId">The external id set by the client for the game series.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="sides">The available sides of the game.</param>
        /// <param name="countries">The countries of the game.</param>
        /// <param name="organizer">The game series organizer.</param>
        /// <param name="players">The players of the game series.</param>
        /// <param name="gameType">The type of the game.</param>
        public GameSeries(
            string? documentId,
            DateTime? created,
            string externalId,
            string name,
            IEnumerable<ISide> sides,
            IEnumerable<ICountry> countries,
            IPerson organizer,
            IEnumerable<IPerson> players,
            string gameType
        )
            : base(documentId, created, null)
        {
            this.Sides = sides.ToArray() ?? throw new ArgumentNullException(nameof(sides));
            this.Countries = countries.ToArray() ?? throw new ArgumentNullException(nameof(countries));
            this.Organizer = organizer ?? throw new ArgumentNullException(nameof(organizer));
            this.Players = players.ToArray() ?? throw new ArgumentNullException(nameof(players));
            this.GameType = gameType.ValidateIsNotNullOrWhitespace(nameof(gameType));
            this.Name = name.ValidateIsNotNullOrWhitespace(nameof(name));
            this.ExternalId = externalId.ValidateIsAGuid(nameof(externalId));
        }

        /// <summary>
        ///     Create a new instance of <see cref="GameSeries" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the document.</param>
        /// <param name="externalId">The external id set by the client for the game series.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="sides">The available sides of the game.</param>
        /// <param name="countries">The countries of the game.</param>
        /// <param name="organizer">The game series organizer.</param>
        /// <param name="players">The players of the game series.</param>
        /// <param name="gameType">The type of the game.</param>
        [JsonConstructor]
        public GameSeries(
            string? documentId,
            DateTime? created,
            string externalId,
            string name,
            IEnumerable<Side> sides,
            IEnumerable<Country> countries,
            Person organizer,
            IEnumerable<Person> players,
            string gameType
        )
            : this(
                documentId,
                created,
                externalId,
                name,
                sides.Select(s => s as ISide),
                countries,
                organizer,
                players.Select(p => p as IPerson),
                gameType)
        {
        }

        /// <summary>
        ///     Gets the countries of the game series.
        /// </summary>
        [JsonProperty(GameSeries.CountriesName, Required = Required.Always, Order = 112)]
        public IEnumerable<ICountry> Countries { get; }

        /// <summary>
        ///     Gets the id that is set by the client for the new game series.
        /// </summary>
        [JsonProperty(GameSeries.ExternalIdName, Required = Required.Always, Order = 50)]
        public string ExternalId { get; }

        /// <summary>
        ///     Gets the type of the game.
        /// </summary>
        [JsonProperty(GameSeries.GameTypeName, Required = Required.Always, Order = 115)]
        public string GameType { get; }

        /// <summary>
        ///     Gets the name of the game series.
        /// </summary>
        [JsonProperty(GameSeries.NameName, Required = Required.Always, Order = 51)]
        public string Name { get; }

        /// <summary>
        ///     Gets the organizer of the game series.
        /// </summary>
        [JsonProperty(GameSeries.OrganizerName, Required = Required.Always, Order = 113)]
        public IPerson Organizer { get; }

        /// <summary>
        ///     Gets the players of the game series.
        /// </summary>
        [JsonProperty(GameSeries.PlayersName, Required = Required.Always, Order = 114)]
        public IEnumerable<IPerson> Players { get; }

        /// <summary>
        ///     Gets the side of the game.
        /// </summary>
        [JsonProperty(GameSeries.SidesName, Required = Required.Always, Order = 111)]
        public IEnumerable<ISide> Sides { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(GameSeries.NameName, this.Name);
            dictionary.Add(GameSeries.SidesName, this.Sides.Select(side => side.ToDictionary()).ToArray());
            dictionary.Add(
                GameSeries.CountriesName,
                this.Countries.Select(country => country.ToDictionary()).ToArray());
            dictionary.Add(GameSeries.OrganizerName, this.Organizer.ToDictionary());
            dictionary.Add(GameSeries.PlayersName, this.Players.Select(player => player.ToDictionary()).ToArray());
            dictionary.Add(GameSeries.GameTypeName, this.GameType);
            dictionary.Add(GameSeries.ExternalIdName, this.ExternalId);
            return dictionary;
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="GameSeries" />.</returns>
        public static IGameSeries FromDictionary(IDictionary<string, object> dictionary)
        {
            var documentId = dictionary.GetString(DatabaseObject.DocumentIdName);
            var created = dictionary.GetDateTime(DatabaseObject.CreatedName);

            var name = dictionary.GetString(GameSeries.NameName);
            var organizer = Person.FromDictionary(dictionary.GetDictionary(GameSeries.OrganizerName));
            var sides = dictionary.GetDictionaries(GameSeries.SidesName).Select(Side.FromDictionary).ToArray();
            var countries = dictionary.GetDictionaries(GameSeries.CountriesName)
                .Select(Country.FromDictionary)
                .ToArray();
            var players = dictionary.GetDictionaries(GameSeries.PlayersName).Select(Person.FromDictionary).ToArray();
            var gameType = dictionary.GetString(GameSeries.GameTypeName);
            var externalId = dictionary.GetString(GameSeries.ExternalIdName);
            return new GameSeries(
                documentId,
                created,
                externalId,
                name,
                sides,
                countries,
                organizer,
                players,
                gameType);
        }
    }
}
