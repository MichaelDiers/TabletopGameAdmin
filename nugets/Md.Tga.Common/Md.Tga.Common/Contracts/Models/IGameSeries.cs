namespace Md.Tga.Common.Contracts.Models
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Database;

    /// <summary>
    ///     Describes a game series.
    /// </summary>
    public interface IGameSeries : IDatabaseObject
    {
        /// <summary>
        ///     Gets the countries of the game series.
        /// </summary>
        IEnumerable<ICountry> Countries { get; }

        /// <summary>
        ///     Gets the type of the game.
        /// </summary>
        string GameType { get; }

        /// <summary>
        ///     Gets the name of the game.
        /// </summary>
        string Name { get; }

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
        IEnumerable<ISide> Sides { get; }
    }
}
