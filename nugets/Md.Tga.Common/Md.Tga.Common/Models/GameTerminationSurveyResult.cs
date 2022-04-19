namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Database;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Describes a game termination result.
    /// </summary>
    public class GameTerminationSurveyResult : DatabaseObject, IGameTerminationSurveyResult
    {
        /// <summary>
        ///     The json name of <see cref="Accept" />.
        /// </summary>
        public const string AcceptName = "accept";

        /// <summary>
        ///     The json name of <see cref="PlayerId" />.
        /// </summary>
        public const string PlayerIdName = "playerId";

        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationSurveyResult" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the entity.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="playerId">The id of the player.</param>
        /// <param name="accept">A value that indicates if the player accepts the termination.</param>
        public GameTerminationSurveyResult(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            string playerId,
            bool accept
        )
            : base(documentId, created, parentDocumentId)
        {
            this.PlayerId = playerId.ValidateIsAGuid(nameof(playerId));
            this.Accept = accept;
        }

        /// <summary>
        ///     Gets a value that indicates if the player accepts the termination.
        /// </summary>
        public bool Accept { get; }

        /// <summary>
        ///     Gets the id of the player.
        /// </summary>

        public string PlayerId { get; }

        /// <summary>
        ///     Add the entity data to the dictionary.
        /// </summary>
        /// <param name="dictionary">The data is added to this dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(GameTerminationSurveyResult.PlayerIdName, this.PlayerId);
            dictionary.Add(GameTerminationSurveyResult.AcceptName, this.Accept);
            return base.AddToDictionary(dictionary);
        }

        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationSurveyResult" />.
        /// </summary>
        /// <param name="dictionary">The data of the new instance.</param>
        /// <returns>An <see cref="IGameTerminationSurveyResult" />.</returns>
        public new static IGameTerminationSurveyResult FromDictionary(IDictionary<string, object> dictionary)
        {
            var baseObject = DatabaseObject.FromDictionary(dictionary);
            var playerId = dictionary.GetString(GameTerminationSurveyResult.PlayerIdName);
            var accepted = dictionary.GetBool(GameTerminationSurveyResult.AcceptName);
            return new GameTerminationSurveyResult(
                baseObject.DocumentId,
                baseObject.Created,
                baseObject.ParentDocumentId,
                playerId,
                accepted);
        }
    }
}
