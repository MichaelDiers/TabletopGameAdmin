namespace Md.Tga.Common.Contracts.Messages
{
    using Md.GoogleCloud.Base.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Describes a message that describes a new game series.
    /// </summary>
    public interface ISaveGameSeriesMessage : IMessage
    {
        /// <summary>
        ///     Get the data of the game series.
        /// </summary>
        IGameSeries GameSeries { get; }
    }
}
