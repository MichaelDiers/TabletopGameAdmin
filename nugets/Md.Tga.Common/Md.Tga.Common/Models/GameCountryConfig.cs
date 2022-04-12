namespace Md.Tga.Common.Models
{
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a country of a game.
    /// </summary>
    public class GameCountryConfig : IGameCountryConfig
    {
        /// <summary>
        ///     THe json name of <see cref="Name" />.
        /// </summary>
        public const string NameName = "name";

        /// <summary>
        ///     The json name of <see cref="Side" />.
        /// </summary>
        public const string SideName = "side";

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

        /// <summary>
        ///     Creates a new instance of <see cref="GameCountryConfig" />.
        /// </summary>
        /// <param name="dictionary">The data of the new config.</param>
        /// <returns>An instance of <see cref="IGameCountryConfig" />.</returns>
        public static IGameCountryConfig FromDictionary(IDictionary<string, object> dictionary)
        {
            var name = dictionary.GetString(GameCountryConfig.NameName);
            var side = dictionary.GetString(GameCountryConfig.SideName);
            return new GameCountryConfig(name, side);
        }
    }
}
