namespace Md.Tga.Common.Contracts.Models
{
    /// <summary>
    ///     Describes a country of a game.
    /// </summary>
    public interface IGameCountryConfig
    {
        /// <summary>
        ///     Gets the name of the country.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the name of the side of the country.
        /// </summary>
        string Side { get; }
    }
}
