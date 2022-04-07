﻿namespace Md.Tga.Common.Contracts.Models
{
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Player and termination link mapping.
    /// </summary>
    public interface IGameTermination : IToDictionary
    {
        /// <summary>
        ///     Gets the id of the player.
        /// </summary>
        string PlayerId { get; }

        /// <summary>
        ///     Gets the id for game termination.
        /// </summary>
        string TerminationId { get; }
    }
}
