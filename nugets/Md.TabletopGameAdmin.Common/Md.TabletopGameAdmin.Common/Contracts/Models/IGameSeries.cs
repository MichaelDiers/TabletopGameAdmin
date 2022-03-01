namespace Md.TabletopGameAdmin.Common.Contracts.Models
{
    using System.Collections.Generic;

    /// <summary>
    ///     Describes a game series.
    /// </summary>
    public interface IGameSeries : INamedBase
    {
        /// <summary>
        ///     Gets the countries of the game series.
        /// </summary>
        IEnumerable<ICountry> Countries { get; }

        /// <summary>
        ///     Gets the organizer of the game series.
        /// </summary>
        IPerson Organizer { get; }

        /// <summary>
        ///     Gets the players of the game series.
        /// </summary>
        IEnumerable<IPerson> Players { get; }

        /// <summary>
        ///     Gets the side of the game.
        /// </summary>
        IEnumerable<INamedBase> Sides { get; }
    }
}
