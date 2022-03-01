namespace Md.TabletopGameAdmin.Common.Contracts.Models
{
    /// <summary>
    ///     Describes a country.
    /// </summary>
    public interface ICountry : INamedBase
    {
        /// <summary>
        ///     Gets the id of the side that the country supports.
        /// </summary>
        string SideId { get; }
    }
}
