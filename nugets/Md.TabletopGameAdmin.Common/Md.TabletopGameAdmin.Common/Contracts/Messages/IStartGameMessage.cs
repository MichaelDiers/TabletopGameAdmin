namespace Md.TabletopGameAdmin.Common.Contracts.Messages
{
    using Md.GoogleCloud.Base.Contracts.Messages;
    using Md.TabletopGameAdmin.Common.Contracts.Models;

    /// <summary>
    ///     Describes a start game message.
    /// </summary>
    public interface IStartGameMessage : IMessage
    {
        /// <summary>
        ///     Gets the game series data. The data is optional.
        /// </summary>
        IGameSeries? GameSeries { get; }

        /// <summary>
        ///     Gets the internal id of the game series.
        /// </summary>
        string InternalId { get; }
    }
}
