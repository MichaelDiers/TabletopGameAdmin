namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.Common.Model;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a game name entity.
    /// </summary>
    public class GameName : ToDictionaryConverter, IGameName
    {
        /// <summary>
        ///     The json name of <see cref="Created" />.
        /// </summary>
        public const string CreatedName = "created";

        /// <summary>
        ///     The json name of <see cref="DocumentId" />.
        /// </summary>
        public const string DocumentIdName = "documentId";

        /// <summary>
        ///     The json name of <see cref="Name" />.
        /// </summary>
        public const string NameName = "name";

        /// <summary>
        ///     Creates a new instance of <see cref="GameName" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time.</param>
        /// <param name="name">The name of the game.</param>
        public GameName(string documentId, DateTime created, string name)
        {
            this.DocumentId = documentId;
            this.Created = created;
            this.Name = name;
        }

        /// <summary>
        ///     Gets the creation time of the entity.
        /// </summary>
        [JsonProperty(GameName.CreatedName, Required = Required.Always, Order = 10)]
        public DateTime Created { get; }

        /// <summary>
        ///     Gets the id of the document.
        /// </summary>
        [JsonProperty(GameName.DocumentIdName, Required = Required.Always, Order = 9)]
        public string DocumentId { get; }

        /// <summary>
        ///     Gets the name of the game.
        /// </summary>
        [JsonProperty(GameName.NameName, Required = Required.Always, Order = 11)]
        public string Name { get; }

        /// <summary>
        ///     The the entity data to the given dictionary.
        /// </summary>
        /// <param name="dictionary">The data is added to this dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(GameName.DocumentIdName, this.DocumentId);
            dictionary.Add(GameName.CreatedName, this.Created);
            dictionary.Add(GameName.NameName, this.Name);
            return dictionary;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="GameName" />.
        /// </summary>
        /// <param name="dictionary">The data from that the instance is initialized.</param>
        /// <returns>A <see cref="IGameName" />.</returns>
        public static IGameName FromDictionary(IDictionary<string, object> dictionary)
        {
            var documentId = dictionary.GetString(GameName.DocumentIdName);
            var created = dictionary.GetDateTime(GameName.CreatedName);
            var name = dictionary.GetString(GameName.NameName);
            return new GameName(documentId, created, name);
        }
    }
}
