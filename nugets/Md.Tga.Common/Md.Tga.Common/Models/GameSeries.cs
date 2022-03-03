namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Extensions;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a game series.
    /// </summary>
    public class GameSeries : NamedBase, IGameSeries

    {
        /// <summary>
        ///     The name of json entry countries.
        /// </summary>
        public const string CountriesName = "countries";

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
        /// <param name="id">The id of the object. Has to be a guid.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="sides">The available sides of the game.</param>
        /// <param name="countries">The countries of the game.</param>
        /// <param name="organizer">The game series organizer.</param>
        /// <param name="players">The players of the game series.</param>
        public GameSeries(
            string id,
            string name,
            IEnumerable<INamedBase> sides,
            IEnumerable<ICountry> countries,
            IPerson organizer,
            IEnumerable<IPerson> players
        )
            : base(id, name)
        {
            this.Sides = sides ?? throw new ArgumentNullException(nameof(sides));
            this.Countries = countries ?? throw new ArgumentNullException(nameof(countries));
            this.Organizer = organizer ?? throw new ArgumentNullException(nameof(organizer));
            this.Players = players ?? throw new ArgumentNullException(nameof(players));
        }

        /// <summary>
        ///     Create a new instance of <see cref="GameSeries" />.
        /// </summary>
        /// <param name="id">The id of the object. Has to be a guid.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="sides">The available sides of the game.</param>
        /// <param name="countries">The countries of the game.</param>
        /// <param name="organizer">The game series organizer.</param>
        /// <param name="players">The players of the game series.</param>
        [JsonConstructor]
        public GameSeries(
            string id,
            string name,
            IEnumerable<NamedBase> sides,
            IEnumerable<Country> countries,
            Person organizer,
            IEnumerable<Person> players
        )
            : this(
                id,
                name,
                sides,
                countries,
                organizer,
                players as IEnumerable<IPerson>)
        {
        }

        /// <summary>
        ///     Gets the countries of the game series.
        /// </summary>
        [JsonProperty(CountriesName, Required = Required.Always, Order = 112)]
        public IEnumerable<ICountry> Countries { get; }

        /// <summary>
        ///     Gets the organizer of the game series.
        /// </summary>
        [JsonProperty(OrganizerName, Required = Required.Always, Order = 113)]
        public IPerson Organizer { get; }

        /// <summary>
        ///     Gets the players of the game series.
        /// </summary>
        [JsonProperty(PlayersName, Required = Required.Always, Order = 114)]
        public IEnumerable<IPerson> Players { get; }

        /// <summary>
        ///     Gets the side of the game.
        /// </summary>
        [JsonProperty(SidesName, Required = Required.Always, Order = 111)]
        public IEnumerable<INamedBase> Sides { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(SidesName, this.Sides.Select(side => side.ToDictionary()).ToArray());
            dictionary.Add(CountriesName, this.Countries.Select(country => country.ToDictionary()).ToArray());
            dictionary.Add(OrganizerName, this.Organizer.ToDictionary());
            dictionary.Add(PlayersName, this.Players.Select(player => player.ToDictionary()).ToArray());
            return dictionary;
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="GameSeries" />.</returns>
        public new static GameSeries FromDictionary(IDictionary<string, object> dictionary)
        {
            var id = dictionary.GetString(IdName);
            var name = dictionary.GetString(NameName);
            var organizer = Person.FromDictionary(dictionary.GetDictionary(OrganizerName));

            var sides = dictionary.GetDictionaries(SidesName).Select(NamedBase.FromDictionary).ToArray();
            var countries = dictionary.GetDictionaries(CountriesName).Select(Country.FromDictionary).ToArray();
            var players = dictionary.GetDictionaries(PlayersName).Select(Person.FromDictionary).ToArray();

            return new GameSeries(
                id,
                name,
                sides,
                countries,
                organizer,
                players);
        }
    }
}
