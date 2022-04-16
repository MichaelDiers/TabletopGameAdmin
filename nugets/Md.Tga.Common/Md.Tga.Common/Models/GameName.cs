namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Database;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a game name entity.
    /// </summary>
    public class GameName : DatabaseObject, IGameName
    {
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
        public GameName(string? documentId, DateTime? created, string name)
            : base(documentId, created, null)
        {
            this.Name = name;
        }

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
            dictionary.Add(GameName.NameName, this.Name);
            return base.AddToDictionary(dictionary);
        }

        /// <summary>
        ///     Creates a new instance of <see cref="GameName" />.
        /// </summary>
        /// <param name="dictionary">The data from that the instance is initialized.</param>
        /// <returns>A <see cref="IGameName" />.</returns>
        public new static IGameName FromDictionary(IDictionary<string, object> dictionary)
        {
            var baseObject = DatabaseObject.FromDictionary(dictionary);
            var name = dictionary.GetString(GameName.NameName);
            return new GameName(baseObject.DocumentId, baseObject.Created, name);
        }
    }
}
