namespace Md.TabletopGameAdmin.Common.Contracts.Models
{
    using Md.TabletopGameAdmin.Common.Contracts.Functional;

    /// <summary>
    ///     Base object for all models.
    /// </summary>
    public interface IBase : IToDictionary
    {
        /// <summary>
        ///     Gets the id.
        /// </summary>
        string Id { get; }
    }
}
