namespace Md.Tga.Common.Contracts.Models
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Base object for all models.
    /// </summary>
    public interface IBase : IToDictionary
    {
        /// <summary>
        ///     Gets the id.
        /// </summary>
        string Id { get; }

        /// <summary>
        ///     Gets the internal document id.
        /// </summary>
        string InternalDocumentId { get; }
    }
}
