namespace Md.Tga.Common.Contracts.Models
{
    using System.Collections.Generic;

    /// <summary>
    ///     Describes the game configuration.
    /// </summary>
    public interface IGameConfig
    {
        /// <summary>
        ///     Gets the configuration of the countries.
        /// </summary>
        IEnumerable<IGameCountryConfig> Countries { get; }

        /// <summary>
        ///     Gets the name of the game.
        /// </summary>
        string Name { get; }
    }
}
