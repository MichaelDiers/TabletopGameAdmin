namespace Md.Tga.Common.Models
{
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a player-country-mapping.
    /// </summary>
    public class PlayerCountryMapping : IPlayerCountryMapping
    {
        public const string CountryIdName = "countryId";
        public const string PlayerIdName = "playerId";

        /// <summary>
        ///     Creates a new instance of <see cref="PlayerCountryMapping" />.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="countryId"></param>
        public PlayerCountryMapping(string playerId, string countryId)
        {
            this.CountryId = countryId.ValidateIsAGuid(nameof(countryId));
            this.PlayerId = playerId.ValidateIsAGuid(nameof(playerId));
        }

        /// <summary>
        ///     Gets the id of the country.
        /// </summary>
        [JsonProperty(PlayerCountryMapping.CountryIdName, Required = Required.Always, Order = 12)]
        public string CountryId { get; }

        /// <summary>
        ///     Gets the id of the player.
        /// </summary>
        [JsonProperty(PlayerCountryMapping.PlayerIdName, Required = Required.Always, Order = 11)]
        public string PlayerId { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(PlayerCountryMapping.CountryIdName, this.CountryId);
            dictionary.Add(PlayerCountryMapping.PlayerIdName, this.PlayerId);
            return dictionary;
        }

        /// <summary>
        ///     Create a dictionary from the object properties.
        /// </summary>
        /// <returns>A <see cref="IDictionary{TKey,TValue}" />.</returns>
        public IDictionary<string, object> ToDictionary()
        {
            return this.AddToDictionary(new Dictionary<string, object>());
        }

        /// <summary>
        ///     Create a <see cref="PlayerCountryMapping" /> from database data.
        /// </summary>
        /// <param name="dictionary">Data from the database.</param>
        /// <returns>An <see cref="IPlayerCountryMapping" />.</returns>
        public static IPlayerCountryMapping FromDictionary(IDictionary<string, object> dictionary)
        {
            var countryId = dictionary.GetString(PlayerCountryMapping.CountryIdName);
            var playerId = dictionary.GetString(PlayerCountryMapping.PlayerIdName);
            return new PlayerCountryMapping(playerId, countryId);
        }
    }
}
