namespace Md.Tga.Common.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes the game configuration.
    /// </summary>
    public class GameConfig : IGameConfig
    {
        /// <summary>
        ///     The json name of <see cref="Countries" />.
        /// </summary>
        public const string CountriesName = "countries";

        /// <summary>
        ///     The json name of <see cref="Name" />.
        /// </summary>
        public const string NameName = "name";

        /// <summary>
        ///     Creates a new instance of <see cref="GameConfig" />.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="countries">The countries of the game.</param>
        [JsonConstructor]
        public GameConfig(string name, IEnumerable<GameCountryConfig> countries)
            : this(name, countries.Select(c => c as IGameCountryConfig))
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="GameConfig" />.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="countries">The countries of the game.</param>
        public GameConfig(string name, IEnumerable<IGameCountryConfig> countries)
        {
            this.Name = name.ValidateIsNotNullOrWhitespace(nameof(name));
            this.Countries = countries.ToArray();
        }

        /// <summary>
        ///     Gets the configuration of the countries.
        /// </summary>
        [JsonProperty(GameConfig.CountriesName, Required = Required.Always, Order = 2)]
        public IEnumerable<IGameCountryConfig> Countries { get; }

        /// <summary>
        ///     Gets the name of the game.
        /// </summary>
        [JsonProperty(GameConfig.NameName, Required = Required.Always, Order = 1)]
        public string Name { get; }

        /// <summary>
        ///     Create a new instance of <see cref="GameConfig" /> from dictionary data.
        /// </summary>
        /// <param name="dictionary">The data of the game configuration.</param>
        /// <returns>An <see cref="IGameConfig" />.</returns>
        public static IGameConfig FromDictionary(IDictionary<string, object> dictionary)
        {
            var name = dictionary.GetString(GameConfig.NameName);
            var countries = dictionary.GetDictionaries(GameConfig.CountriesName)
                .Select(GameCountryConfig.FromDictionary)
                .ToArray();
            return new GameConfig(name, countries);
        }
    }
}
