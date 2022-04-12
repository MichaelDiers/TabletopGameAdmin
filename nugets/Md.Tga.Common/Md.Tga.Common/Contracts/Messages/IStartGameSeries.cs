namespace Md.Tga.Common.Contracts.Messages
{
    using System.Collections.Generic;

    /// <summary>
    ///     Describes a request for starting a new game series.
    /// </summary>
    public interface IStartGameSeries
    {
        /// <summary>
        ///     Gets the id that is set by the client for the new game series.
        /// </summary>
        string ExternalId { get; }

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
        IStartGameSeriesPerson Organizer { get; }

        /// <summary>
        ///     Gets the players of the game series.
        /// </summary>
        IEnumerable<IStartGameSeriesPerson> Players { get; }
    }
}
