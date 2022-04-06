﻿namespace Md.Tga.Common.Models
{
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Base object for all models.
    /// </summary>
    public class Base : IBase
    {
        /// <summary>
        ///     The name of json entry id.
        /// </summary>
        public const string IdName = "id";

        /// <summary>
        ///     The name of the json entry internal id.
        /// </summary>
        public const string InternalDocumentIdName = "internalId";

        /// <summary>
        ///     Create a new instance of <see cref="Base" />.
        /// </summary>
        /// <param name="id">The id of the object. Has to be a guid.</param>
        protected Base(string id)
            : this(id, string.Empty)
        {
        }

        /// <summary>
        ///     Create a new instance of <see cref="Base" />.
        /// </summary>
        /// <param name="id">The id of the object. Has to be a guid.</param>
        /// <param name="internalDocumentId">The internal document id.</param>
        protected Base(string id, string internalDocumentId)
        {
            this.Id = id.ValidateIsAGuid(nameof(id));
            this.InternalDocumentId = internalDocumentId;
        }

        /// <summary>
        ///     Gets the id.
        /// </summary>
        [JsonProperty(Base.IdName, Required = Required.Always, Order = 1)]
        public string Id { get; }

        /// <summary>
        ///     Gets the internal document id.
        /// </summary>
        [JsonIgnore]
        public string InternalDocumentId { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public virtual IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(Base.IdName, this.Id);
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
    }
}
