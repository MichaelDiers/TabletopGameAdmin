namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Database;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a game termination result.
    /// </summary>
    public class GameTerminationResult : DatabaseObject, IGameTerminationResult
    {
        /// <summary>
        ///     The json name of <see cref="PlayerId" />.
        /// </summary>
        public const string PlayerIdName = "playerId";

        /// <summary>
        ///     The json name of <see cref="Reason" />.
        /// </summary>
        public const string ReasonName = "reason";

        /// <summary>
        ///     The json name of <see cref="Rounds" />.
        /// </summary>
        public const string RoundsName = "rounds";

        /// <summary>
        ///     The json name of <see cref="WinningSideId" />.
        /// </summary>
        public const string WinningSideIdName = "winningSideId";

        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationResult" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the entity.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="playerId">The id of the player.</param>
        /// <param name="winningSideId">The id of the winning side.</param>
        /// <param name="reason">A reason for terminating the game.</param>
        /// <param name="rounds">The number of played rounds.</param>
        public GameTerminationResult(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            string playerId,
            string winningSideId,
            string reason,
            int rounds
        )
            : base(documentId, created, parentDocumentId)
        {
            this.PlayerId = playerId.ValidateIsAGuid(nameof(playerId));
            this.WinningSideId = winningSideId.ValidateIsAGuid(nameof(winningSideId));
            this.Reason = reason;
            this.Rounds = rounds;
        }

        /// <summary>
        ///     Gets the id of the player.
        /// </summary>

        [JsonProperty(GameTerminationResult.PlayerIdName, Required = Required.Always, Order = 11)]
        public string PlayerId { get; }

        /// <summary>
        ///     Gets a reason for terminating the game.
        /// </summary>
        [JsonProperty(GameTerminationResult.ReasonName, Required = Required.Always, Order = 13)]
        public string Reason { get; }

        /// <summary>
        ///     Gets the number of played rounds.
        /// </summary>
        [JsonProperty(GameTerminationResult.RoundsName, Required = Required.Always, Order = 14)]
        public int Rounds { get; }

        /// <summary>
        ///     Gets the id of the winning side.
        /// </summary>
        [JsonProperty(GameTerminationResult.WinningSideIdName, Required = Required.Always, Order = 12)]
        public string WinningSideId { get; }

        /// <summary>
        ///     Add the entity data to the dictionary.
        /// </summary>
        /// <param name="dictionary">The data is added to this dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(GameTerminationResult.PlayerIdName, this.PlayerId);
            dictionary.Add(GameTerminationResult.WinningSideIdName, this.WinningSideId);
            dictionary.Add(GameTerminationResult.ReasonName, this.Reason);
            dictionary.Add(GameTerminationResult.RoundsName, this.Rounds);
            return base.AddToDictionary(dictionary);
        }

        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationResult" />.
        /// </summary>
        /// <param name="dictionary">The data of the new instance.</param>
        /// <returns>An <see cref="IGameTerminationResult" />.</returns>
        public new static IGameTerminationResult FromDictionary(IDictionary<string, object> dictionary)
        {
            var baseObject = DatabaseObject.FromDictionary(dictionary);
            var playerId = dictionary.GetString(GameTerminationResult.PlayerIdName);
            var winningSideId = dictionary.GetString(GameTerminationResult.WinningSideIdName);
            var reason = dictionary.GetString(GameTerminationResult.ReasonName, string.Empty);
            var rounds = dictionary.GetInt(GameTerminationResult.RoundsName);
            return new GameTerminationResult(
                baseObject.DocumentId,
                baseObject.Created,
                baseObject.ParentDocumentId,
                playerId,
                winningSideId,
                reason,
                rounds);
        }
    }
}
