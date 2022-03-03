namespace Md.Tga.Common.Tests.Models
{
    using Md.Tga.Common.Models;

    public class BaseImplementation : Base
    {
        /// <summary>
        ///     Create a new instance of <see cref="Base" />.
        /// </summary>
        /// <param name="id">The id of the object. Has to be a guid.</param>
        public BaseImplementation(string id)
            : base(id)
        {
        }
    }
}
