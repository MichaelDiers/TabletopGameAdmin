namespace Md.TabletopGameAdmin.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Md.TabletopGameAdmin.Common.Contracts.Models;

    /// <summary>
    ///     Describes a named base object.
    /// </summary>
    public class NamedBase : Base, INamedBase
    {
        /// <summary>
        ///     Create a new instance of <see cref="NamedBase" />.
        /// </summary>
        /// <param name="id">The id of the object. Has to be a guid.</param>
        /// <param name="name">The name of the object.</param>
        public NamedBase(string id, string name)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            this.Name = name;
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        [JsonProperty("name", Required = Required.Always, Order = 11)]
        public string Name { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add("name", this.Name);
            return dictionary;
        }
    }
}
