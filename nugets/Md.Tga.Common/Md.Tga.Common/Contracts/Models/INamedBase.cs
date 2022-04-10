namespace Md.Tga.Common.Contracts.Models
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Describes a named base object.
    /// </summary>
    public interface INamedBase : IToDictionary
    {
        /// <summary>
        ///     Gets the id.
        /// </summary>
        string Id { get; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        string Name { get; }
    }
}
