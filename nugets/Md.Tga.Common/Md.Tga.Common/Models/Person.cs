namespace Md.Tga.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a person.
    /// </summary>
    public class Person : NamedBase, IPerson
    {
        /// <summary>
        ///     The name of json entry email.
        /// </summary>
        public const string EmailName = "email";

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
        [JsonProperty(EmailName, Required = Required.Always, Order = 111)]
        public string Email { get; }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(EmailName, this.Email);
            return dictionary;
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="Person" />.</returns>
        public new static Person FromDictionary(IDictionary<string, object> dictionary)
        {
            if (!dictionary.TryGetValue(IdName, out var idValue))
            {
                throw new ArgumentException($"Invalid data from dictionary: Missing {IdName}", nameof(dictionary));
            }

            if (!(idValue is string id))
            {
                throw new ArgumentException(
                    $"Invalid data from dictionary: {IdName} {idValue} is not a string",
                    nameof(dictionary));
            }

            if (!dictionary.TryGetValue(NameName, out var nameValue))
            {
                throw new ArgumentException($"Invalid data from dictionary: Missing {NameName}", nameof(dictionary));
            }

            if (!(nameValue is string name))
            {
                throw new ArgumentException(
                    $"Invalid data from dictionary: {NameName} {nameValue} is not a string",
                    nameof(dictionary));
            }

            if (!dictionary.TryGetValue(EmailName, out var emailValue))
            {
                throw new ArgumentException($"Invalid data from dictionary: Missing {EmailName}", nameof(dictionary));
            }

            if (!(emailValue is string email))
            {
                throw new ArgumentException(
                    $"Invalid data from dictionary: {EmailName} {emailValue} is not a string",
                    nameof(dictionary));
            }

            return new Person(id, name, email);
        }
    }
}
