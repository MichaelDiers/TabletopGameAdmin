namespace Md.Tga.Common.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes all player-mappings of a game.
    /// </summary>
    public class PlayerMappings : IPlayerMappings
    {
        /// <summary>
        ///     The json name of <see cref="InternalGameId" />.
        /// </summary>
        public const string InternalGameIdName = "internalGameId";

        /// <summary>
        ///     The json name of <see cref="PlayerCountryMappings" />.
        /// </summary>
        public const string PlayerCountryMappingsName = "playerCountryMappings";


        /// <summary>
        ///     Creates a new instance of <see cref="PlayerMappings" />.
        /// </summary>
        /// <param name="internalGameId">The internal game id.</param>
        /// <param name="playerCountryMappings">The player-country-mapping of the game.</param>
        public PlayerMappings(string internalGameId, IEnumerable<PlayerCountryMapping> playerCountryMappings)
            : this(internalGameId, playerCountryMappings.Select(pcm => pcm as IPlayerCountryMapping).ToArray())
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="PlayerMappings" />.
        /// </summary>
        /// <param name="internalGameId">The internal game id.</param>
        /// <param name="playerCountryMappings">The player-country-mapping of the game.</param>
        public PlayerMappings(string internalGameId, IEnumerable<IPlayerCountryMapping> playerCountryMappings)
        {
            this.InternalGameId = internalGameId.ValidateIsNotNullOrWhitespace(nameof(internalGameId));
            this.PlayerCountryMappings = playerCountryMappings;
        }

        /// <summary>
        ///     Gets the internal game id.
        /// </summary>
        [JsonProperty(PlayerMappings.InternalGameIdName, Required = Required.Always, Order = 11)]
        public string InternalGameId { get; }

        /// <summary>
        ///     Gets the player mappings of the game.
        /// </summary>
        [JsonProperty(PlayerMappings.PlayerCountryMappingsName, Required = Required.Always, Order = 12)]
        public IEnumerable<IPlayerCountryMapping> PlayerCountryMappings { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(PlayerMappings.InternalGameIdName, this.InternalGameId);
            dictionary.Add(
                PlayerMappings.PlayerCountryMappingsName,
                this.PlayerCountryMappings.Select(pcm => pcm.ToDictionary()).ToArray());
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
        ///     Create a <see cref="PlayerMappings" /> from database data.
        /// </summary>
        /// <param name="dictionary">Data from the database.</param>
        /// <returns>An <see cref="IPlayerMappings" />.</returns>
        public static IPlayerMappings FromDictionary(IDictionary<string, object> dictionary)
        {
            var internalGameId = dictionary.GetString(PlayerMappings.InternalGameIdName);
            var playerCountryMappings = dictionary.GetDictionaries(PlayerMappings.PlayerCountryMappingsName)
                .Select(PlayerCountryMapping.FromDictionary)
                .ToArray();
            return new PlayerMappings(internalGameId, playerCountryMappings);
        }
    }
}
