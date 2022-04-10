namespace Md.Tga.Common.Models
{
    using System.Collections.Generic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Describes a side of a game series.
    /// </summary>
    public class Side : NamedBase, ISide
    {
        /// <summary>
        ///     Create a new instance of <see cref="Side" />.
        /// </summary>
        /// <param name="id">The id of the object. Has to be a guid.</param>
        /// <param name="name">The name of the object.</param>
        public Side(string id, string name)
            : base(id, name)
        {
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="ISide" />.</returns>
        public new static ISide FromDictionary(IDictionary<string, object> dictionary)
        {
            var namedBase = NamedBase.FromDictionary(dictionary);
            return new Side(namedBase.Id, namedBase.Name);
        }
    }
}
