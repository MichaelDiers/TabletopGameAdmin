namespace Md.Tga.Common.Contracts.Models
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Database;

    /// <summary>
    ///     Describes all player-mappings for a game.
    /// </summary>
    public interface IPlayerMappings : IDatabaseObject
    {
        /// <summary>
        ///     Gets the player-country-mappings of the game.
        /// </summary>
        IEnumerable<IPlayerCountryMapping> PlayerCountryMappings { get; }
    }
}
