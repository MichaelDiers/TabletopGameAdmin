namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Database;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes the status of a game.
    /// </summary>
    public class GameStatus : DatabaseObject, IGameStatus
    {
        /// <summary>
        ///     The json name of <see cref="Status" />.
        /// </summary>
        public const string StatusName = "status";

        /// <summary>
        ///     The json name of <see cref="WinningSideId" />.
        /// </summary>
        public const string WinningSideIdName = "winningSideId";


        /// <summary>
        ///     Creates a new instance of <see cref="GameStatus" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the entity.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="status">The status of the game.</param>
        /// <param name="winningSideId">The id of the winning side.</param>
        public GameStatus(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            Status status,
            string winningSideId
        )
            : base(documentId, created, parentDocumentId)
        {
            this.Status = status;
            this.WinningSideId = winningSideId;
        }

        /// <summary>
        ///     Gets the status.
        /// </summary>
        [JsonProperty(GameStatus.StatusName, Required = Required.Always, Order = 11)]
        public Status Status { get; }

        /// <summary>
        /// Gets the winning side id if the status is <see cref="Status.Closed"/>.
        /// </summary>
        public string WinningSideId { get; }


        /// <summary>
        ///     Add the values of the entity to the given dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to this dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(GameStatus.StatusName, this.Status.ToString());
            dictionary.Add(GameStatus.WinningSideIdName, this.WinningSideId);
            return base.AddToDictionary(dictionary);
        }

        /// <summary>
        ///     Create a new instance of <see cref="GameStatus" />.
        /// </summary>
        /// <param name="dictionary">The values of the new instance.</param>
        /// <returns>An <see cref="IGameStatus" />.</returns>
        public new static IGameStatus FromDictionary(IDictionary<string, object> dictionary)
        {
            var baseObject = DatabaseObject.FromDictionary(dictionary);
            var status = dictionary.GetEnumValue<Status>(GameStatus.StatusName);
            var winningSideId = dictionary.GetString(WinningSideIdName);
            return new GameStatus(
                baseObject.DocumentId,
                baseObject.Created,
                baseObject.ParentDocumentId,
                status,
                winningSideId);
        }
    }
}
