namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Database;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a game termination survey.
    /// </summary>
    public class GameTerminationSurvey : DatabaseObject, IGameTerminationSurvey
    {
        /// <summary>
        ///     The json name of <see cref="WinningSideId" />.
        /// </summary>
        public const string WinningSideIdName = "winningSideId";

        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationSurvey" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the entity.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="winningSideId">The id of the winning side.</param>
        public GameTerminationSurvey(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            string winningSideId
        )
            : base(documentId, created, parentDocumentId)
        {
            this.WinningSideId = winningSideId.ValidateIsAGuid(nameof(winningSideId));
        }

        /// <summary>
        ///     Gets the id of the winning side.
        /// </summary>
        [JsonProperty(GameTerminationSurvey.WinningSideIdName, Required = Required.Always, Order = 11)]
        public string WinningSideId { get; }

        /// <summary>
        ///     Add the entity data to the dictionary.
        /// </summary>
        /// <param name="dictionary">The data is added to this dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(GameTerminationSurvey.WinningSideIdName, this.WinningSideId);
            return base.AddToDictionary(dictionary);
        }

        /// <summary>
        ///     Creates a new instance of <see cref="GameTerminationSurvey" />.
        /// </summary>
        /// <param name="dictionary">The data of the new instance.</param>
        /// <returns>An <see cref="IGameTerminationSurvey" />.</returns>
        public new static IGameTerminationSurvey FromDictionary(IDictionary<string, object> dictionary)
        {
            var baseObject = DatabaseObject.FromDictionary(dictionary);
            var winningSideId = dictionary.GetString(GameTerminationSurvey.WinningSideIdName);
            return new GameTerminationSurvey(
                baseObject.DocumentId,
                baseObject.Created,
                baseObject.ParentDocumentId,
                winningSideId);
        }
    }
}
