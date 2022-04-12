namespace Md.Tga.Common.Models
{
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a country of a game.
    /// </summary>
    public class GameCountryConfig : IGameCountryConfig
    {
        /// <summary>
        ///     Creates a new instance of <see cref="GameCountryConfig" />.
        /// </summary>
        /// <param name="name">The name of the country.</param>
        /// <param name="side">The name of the side of the country.</param>
        public GameCountryConfig(string name, string side)
        {
            this.Name = name.ValidateIsNotNullOrWhitespace(nameof(name));
            this.Side = side.ValidateIsNotNullOrWhitespace(nameof(side));
        }

        /// <summary>
        ///     Gets the name of the country.
        /// </summary>
        [JsonProperty("name", Required = Required.Always, Order = 1)]
        public string Name { get; }

        /// <summary>
        ///     Gets the name of the side of the country.
        /// </summary>
        [JsonProperty("side", Required = Required.Always, Order = 2)]
        public string Side { get; }
    }
}
