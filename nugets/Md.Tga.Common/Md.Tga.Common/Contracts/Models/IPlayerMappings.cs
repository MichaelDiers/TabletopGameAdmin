namespace Md.Tga.Common.Contracts.Models
{
    using System.Collections.Generic;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Describes all player-mappings for a game.
    /// </summary>
    public interface IPlayerMappings : IToDictionary
    {
        /// <summary>
        ///     Gets the internal game id.
        /// </summary>
        string InternalGameId { get; }

        /// <summary>
        ///     Gets the player-country-mappings of the game.
        /// </summary>
        IEnumerable<IPlayerCountryMapping> PlayerCountryMappings { get; }
    }
}
