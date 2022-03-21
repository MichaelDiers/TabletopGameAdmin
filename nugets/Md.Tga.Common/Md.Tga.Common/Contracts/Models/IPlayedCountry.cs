namespace Md.Tga.Common.Contracts.Models
{
    /// <summary>
    ///     Specifies which player plays which country.
    /// </summary>
    public interface IPlayedCountry
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
