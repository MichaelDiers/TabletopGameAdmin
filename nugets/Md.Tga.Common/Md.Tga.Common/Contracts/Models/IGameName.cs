namespace Md.Tga.Common.Contracts.Models
{
    using System;
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Describes a game name entity.
    /// </summary>
    public interface IGameName : IToDictionary
    {
        /// <summary>
        ///     Gets the creation time of the entity.
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        ///     Gets the id of the document.
        /// </summary>
        string DocumentId { get; }

        /// <summary>
        ///     Gets the name of the game.
        /// </summary>
        string Name { get; }
    }
}
