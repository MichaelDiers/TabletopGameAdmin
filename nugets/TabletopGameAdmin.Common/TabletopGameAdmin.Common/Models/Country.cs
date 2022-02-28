namespace TabletopGameAdmin.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using TabletopGameAdmin.Common.Contracts.Models;

    /// <summary>
    ///     Describes a country.
    /// </summary>
    public class Country : NamedBase, ICountry
    {
        /// <summary>
        ///     Create a new instance of <see cref="Country" />.
        /// </summary>
        /// <param name="id">The id of the object. Has to be a guid.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="sideId">The id of the side that the country supports.</param>
        public Country(string id, string name, string sideId)
            : base(id, name)
        {
            if (string.IsNullOrWhiteSpace(sideId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(sideId));
            }

            if (!Guid.TryParse(sideId, out var guid) || guid == Guid.Empty)
            {
                throw new ArgumentException($"Value is not a valid guid: {sideId}", nameof(sideId));
            }

            this.SideId = sideId;
        }

        /// <summary>
        ///     Gets the id of the side that the country supports.
        /// </summary>
        [JsonProperty("sideId", Required = Required.Always, Order = 111)]
        public string SideId { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add("sideId", this.SideId);
            return dictionary;
        }
    }
}
