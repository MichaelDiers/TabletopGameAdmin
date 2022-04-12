namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     A message that describes a new game series.
    /// </summary>
    public interface IStartGameSeriesMessage : IMessage
    {
        /// <summary>
        ///     Get the data of the game series.
        /// </summary>
        IStartGameSeries GameSeries { get; }
    }
}
