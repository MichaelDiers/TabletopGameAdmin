namespace Md.Tga.Common.Models
{
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    public class PlayedCountry : ToDictionaryConverter, IPlayedCountry
    {
        /// <summary>
        ///     The json name of the country id.
        /// </summary>
        public const string CountryIdName = "countryId";

        /// <summary>
        ///     The json name of the player id.
        /// </summary>
        public const string PlayerIdName = "playerId";

        /// <summary>
        ///     Creates a new instance of <see cref="PlayedCountry" />.
        /// </summary>
        /// <param name="playerId">The id of the player.</param>
        /// <param name="countryId">The id of the country.</param>
        public PlayedCountry(string playerId, string countryId)
        {
            this.PlayerId = playerId.ValidateIsAGuid(nameof(playerId));
            this.CountryId = countryId.ValidateIsAGuid(nameof(countryId));
        }

        /// <summary>
        ///     Gets the id of the country.
        /// </summary>
        [JsonProperty(PlayedCountry.CountryIdName, Required = Required.Always, Order = 2)]
        public string CountryId { get; }

        /// <summary>
        ///     Gets the id of the player.
        /// </summary>
        [JsonProperty(PlayedCountry.PlayerIdName, Required = Required.Always, Order = 1)]
        public string PlayerId { get; }

        /// <summary>
        ///     Add the property values of the instance to the dictionary.
        /// </summary>
        /// <param name="dictionary">The data is added to the dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(PlayedCountry.PlayerIdName, this.PlayerId);
            dictionary.Add(PlayedCountry.CountryIdName, this.CountryId);
            return dictionary;
        }

        /// <summary>
        ///     Initialize a new instance of <see cref="PlayedCountry" />.
        /// </summary>
        /// <param name="dictionary">The data of the object.</param>
        /// <returns>An <see cref="IPlayedCountry" />.</returns>
        public static IPlayedCountry FromDictionary(IDictionary<string, object> dictionary)
        {
            var playerId = dictionary.GetString(PlayedCountry.PlayerIdName);
            var countryId = dictionary.GetString(PlayedCountry.CountryIdName);
            return new PlayedCountry(playerId, countryId);
        }
    }
}
