namespace TabletopGameAdmin.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using TabletopGameAdmin.Common.Contracts.Models;

    /// <summary>
    ///     Describes a person.
    /// </summary>
    public class Person : NamedBase, IPerson
    {
        /// <summary>
        ///     Create a new instance of <see cref="Person" />.
        /// </summary>
        /// <param name="id">The id of the object. Has to be a guid.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="email">The email of the person.</param>
        public Person(string id, string name, string email)
            : base(id, name)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));
            }

            this.Email = email;
        }

        /// <summary>
        ///     Gets the email of the person.
        /// </summary>
        [JsonProperty("email", Required = Required.Always, Order = 111)]
        public string Email { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add("email", this.Email);
            return dictionary;
        }
    }
}
