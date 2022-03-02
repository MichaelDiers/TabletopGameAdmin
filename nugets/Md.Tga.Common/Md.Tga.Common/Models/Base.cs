namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Base object for all models.
    /// </summary>
    public class Base : IBase
    {
        /// <summary>
        ///     Create a new instance of <see cref="Base" />.
        /// </summary>
        /// <param name="id">The id of the object. Has to be a guid.</param>
        protected Base(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));
            }

            if (!Guid.TryParse(id, out var guid) || guid == Guid.Empty)
            {
                throw new ArgumentException($"Value is not a valid guid: {id}", nameof(id));
            }

            this.Id = id;
        }

        /// <summary>
        ///     Gets the id.
        /// </summary>
        [JsonProperty("id", Required = Required.Always, Order = 1)]
        public string Id { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public virtual IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add("id", this.Id);
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
