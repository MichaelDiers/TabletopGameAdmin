namespace Md.Tga.Common.Contracts.Models
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Describes a player-country-mapping.
    /// </summary>
    public interface IPlayerCountryMapping : IToDictionary
    {
        /// <summary>
        ///     Gets the id of the country.
        /// </summary>
        string CountryId { get; }

        /// <summary>
        ///     Gets the id of the player.
        /// </summary>
        string PlayerId { get; }
    }
}
