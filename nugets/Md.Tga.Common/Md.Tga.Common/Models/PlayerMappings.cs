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
    ///     Describes all player-mappings of a game.
    /// </summary>
    public class PlayerMappings : DatabaseObject, IPlayerMappings
    {
        /// <summary>
        ///     The json name of <see cref="PlayerCountryMappings" />.
        /// </summary>
        public const string PlayerCountryMappingsName = "playerCountryMappings";

        /// <summary>
        ///     Creates a new instance of <see cref="PlayerMappings" />.
        /// </summary>
        /// <param name="documentId">The document id of the game.</param>
        /// <param name="created">The creation time of the document.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="playerCountryMappings">The player-country-mapping of the game.</param>
        [JsonConstructor]
        public PlayerMappings(
            string? documentId,
            DateTime? created,
            string parentDocumentId,
            IEnumerable<PlayerCountryMapping> playerCountryMappings
        )
            : this(
                documentId,
                created,
                parentDocumentId,
                playerCountryMappings.Select(pcm => pcm as IPlayerCountryMapping).ToArray())
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="PlayerMappings" />.
        /// </summary>
        /// <param name="documentId">The document id of the game.</param>
        /// <param name="created">The creation time of the document.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="playerCountryMappings">The player-country-mapping of the game.</param>
        public PlayerMappings(
            string? documentId,
            DateTime? created,
            string parentDocumentId,
            IEnumerable<IPlayerCountryMapping> playerCountryMappings
        )
            : base(documentId, created, parentDocumentId)
        {
            this.PlayerCountryMappings = playerCountryMappings;
        }

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
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(
                PlayerMappings.PlayerCountryMappingsName,
                this.PlayerCountryMappings.Select(pcm => pcm.ToDictionary()).ToArray());
            return dictionary;
        }

        /// <summary>
        ///     Create a <see cref="PlayerMappings" /> from database data.
        /// </summary>
        /// <param name="dictionary">Data from the database.</param>
        /// <returns>An <see cref="IPlayerMappings" />.</returns>
        public new static IPlayerMappings FromDictionary(IDictionary<string, object> dictionary)
        {
            var documentId = dictionary.GetString(DatabaseObject.DocumentIdName);
            var created = dictionary.GetDateTime(DatabaseObject.CreatedName);
            var parentDocumentId = dictionary.GetString(DatabaseObject.ParentDocumentIdName);

            var playerCountryMappings = dictionary.GetDictionaries(PlayerMappings.PlayerCountryMappingsName)
                .Select(PlayerCountryMapping.FromDictionary)
                .ToArray();
            return new PlayerMappings(
                documentId,
                created,
                parentDocumentId,
                playerCountryMappings);
        }
    }
}
