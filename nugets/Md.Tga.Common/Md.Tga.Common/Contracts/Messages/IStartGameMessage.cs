namespace Md.Tga.Common.Contracts.Messages
{
    using Md.GoogleCloud.Base.Contracts.Messages;

    /// <summary>
    ///     Describes a start game message.
    /// </summary>
    public interface IStartGameMessage : IMessage
    {
        /// <summary>
        ///     Gets the internal id of the game series.
        /// </summary>
        string InternalGameSeriesId { get; }
    }
}
