namespace TabletopGameAdmin.Common.Contracts.Messages
{
    using Md.GoogleCloudPubSub.Base.Contracts.Messages;
    using TabletopGameAdmin.Common.Contracts.Models;

    /// <summary>
    ///     Describes a message that describes a new game series.
    /// </summary>
    public interface IInitializeGameSeriesMessage : IMessage
    {
        /// <summary>
        ///     Get the data of the game series.
        /// </summary>
        IGameSeries GameSeries { get; }
    }
}
