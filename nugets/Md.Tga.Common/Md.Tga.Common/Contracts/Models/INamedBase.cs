namespace Md.Tga.Common.Contracts.Models
{
    /// <summary>
    ///     Describes a named base object.
    /// </summary>
    public interface INamedBase : IBase
    {
        /// <summary>
        ///     Gets the name.
        /// </summary>
        string Name { get; }
    }
}