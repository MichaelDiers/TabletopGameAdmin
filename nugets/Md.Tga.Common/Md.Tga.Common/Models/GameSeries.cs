namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a game series.
    /// </summary>
    public class GameSeries : NamedBase, IGameSeries

    {
        /// <summary>
        ///     The name of json entry countries.
        /// </summary>
        private const string CountriesName = "countries";

        /// <summary>
        ///     The name of json entry organizer.
        /// </summary>
        private const string OrganizerName = "organizer";

        /// <summary>
        ///     The name of json entry players.
        /// </summary>
        private const string PlayersName = "players";

        /// <summary>
        ///     The name of json entry sides.
        /// </summary>
        private const string SidesName = "sides";

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
        [JsonProperty(GameSeries.CountriesName, Required = Required.Always, Order = 112)]
        public IEnumerable<ICountry> Countries { get; }

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
        public IEnumerable<INamedBase> Sides { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(GameSeries.SidesName, this.Sides.Select(side => side.ToDictionary()).ToArray());
            dictionary.Add(
                GameSeries.CountriesName,
                this.Countries.Select(country => country.ToDictionary()).ToArray());
            dictionary.Add(GameSeries.OrganizerName, this.Organizer.ToDictionary());
            dictionary.Add(GameSeries.PlayersName, this.Players.Select(player => player.ToDictionary()).ToArray());
            return dictionary;
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="GameSeries" />.</returns>
        public new static GameSeries FromDictionary(IDictionary<string, object> dictionary)
        {
            var id = dictionary.GetString(Base.IdName);
            var name = dictionary.GetString(NamedBase.NameName);
            var organizer = Person.FromDictionary(dictionary.GetDictionary(GameSeries.OrganizerName));

            var sides = dictionary.GetDictionaries(GameSeries.SidesName).Select(NamedBase.FromDictionary).ToArray();
            var countries = dictionary.GetDictionaries(GameSeries.CountriesName)
                .Select(Country.FromDictionary)
                .ToArray();
            var players = dictionary.GetDictionaries(GameSeries.PlayersName).Select(Person.FromDictionary).ToArray();

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
