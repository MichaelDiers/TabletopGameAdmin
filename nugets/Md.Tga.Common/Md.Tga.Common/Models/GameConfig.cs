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
        ///     Creates a new instance of <see cref="GameConfig" />.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="countries">The countries of the game.</param>
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
        [JsonProperty("countries", Required = Required.Always, Order = 2)]
        public IEnumerable<IGameCountryConfig> Countries { get; }

        /// <summary>
        ///     Gets the name of the game.
        /// </summary>
        [JsonProperty("name", Required = Required.Always, Order = 1)]
        public string Name { get; }
    }
}
