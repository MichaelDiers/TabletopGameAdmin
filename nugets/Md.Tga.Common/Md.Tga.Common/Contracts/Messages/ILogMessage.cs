namespace Md.Tga.Common.Contracts.Messages
{
    using System;
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Message that is sent to save a log message.
    /// </summary>
    public interface ILogMessage : IMessage
    {
        /// <summary>
        ///     Gets the exception of the log entry.
        /// </summary>
        Exception? Exception { get; }

        /// <summary>
        ///     Gets the message of the log entry.
        /// </summary>
        string Message { get; }
    }
}
