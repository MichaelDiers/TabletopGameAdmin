namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a named base object.
    /// </summary>
    public class NamedBase : Base, INamedBase
    {
        /// <summary>
        ///     The name of json entry name.
        /// </summary>
        public const string NameName = "name";

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
        [JsonProperty(NameName, Required = Required.Always, Order = 11)]
        public string Name { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(NameName, this.Name);
            return dictionary;
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="NamedBase" />.</returns>
        public static NamedBase FromDictionary(IDictionary<string, object> dictionary)
        {
            if (dictionary.TryGetValue(IdName, out var idValue)
                && idValue is string id
                && dictionary.TryGetValue(NameName, out var nameValue)
                && nameValue is string name)
            {
                return new NamedBase(id, name);
            }

            throw new ArgumentException("Invalid data from dictionary", nameof(dictionary));
        }
    }
}
