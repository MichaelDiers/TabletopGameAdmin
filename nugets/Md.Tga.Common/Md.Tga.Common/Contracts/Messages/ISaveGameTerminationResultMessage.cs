namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Describes a message for saving an <see cref="IGameTerminationResult" />.
    /// </summary>
    public interface ISaveGameTerminationResultMessage : IMessage
    {
        /// <summary>
        ///     Gets the game termination survey data.
        /// </summary>
        IGameTerminationResult GameTerminationResult { get; }
    }
}
