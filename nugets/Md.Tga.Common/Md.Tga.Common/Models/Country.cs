namespace Md.Tga.Common.Models
{
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Extensions;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a country.
    /// </summary>
    public class Country : NamedBase, ICountry
    {
        /// <summary>
        ///     The name of json entry sideId.
        /// </summary>
        public const string SideIdName = "sideId";

        /// <summary>
        ///     Create a new instance of <see cref="Country" />.
        /// </summary>
        /// <param name="id">The id of the object. Has to be a guid.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="sideId">The id of the side that the country supports.</param>
        public Country(string id, string name, string sideId)
            : base(id, name)
        {
            this.SideId = sideId.ValidateIsAGuid();
        }

        /// <summary>
        ///     Gets the id of the side that the country supports.
        /// </summary>
        [JsonProperty(SideIdName, Required = Required.Always, Order = 111)]
        public string SideId { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(SideIdName, this.SideId);
            return dictionary;
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="Country" />.</returns>
        public new static Country FromDictionary(IDictionary<string, object> dictionary)
        {
            var id = dictionary.GetString(IdName);
            var name = dictionary.GetString(NameName);
            var sideId = dictionary.GetString(SideIdName);
            return new Country(id, name, sideId);
        }
    }
}
