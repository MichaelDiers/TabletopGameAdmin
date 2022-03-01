namespace Md.TabletopGameAdmin.Common.Contracts.Models
{
    using Md.GoogleCloud.Base.Contracts.Logic;

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
