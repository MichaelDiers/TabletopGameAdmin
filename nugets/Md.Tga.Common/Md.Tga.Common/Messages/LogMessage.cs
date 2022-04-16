namespace Md.Tga.Common.Contracts.Messages
{
    using System;
    using Md.Common.Messages;
    using Newtonsoft.Json;

    /// <summary>
    ///     Message that is sent to save a log message.
    /// </summary>
    public class LogMessage : Message, ILogMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="LogMessage" />.
        /// </summary>
        [JsonConstructor]
        public LogMessage(string processId, string message, Exception? exception)
            : base(processId)
        {
            this.Message = message;
            this.Exception = exception;
        }

        /// <summary>
        ///     Gets the exception of the log entry.
        /// </summary>
        [JsonProperty("exception", Required = Required.AllowNull, Order = 12)]
        public Exception? Exception { get; }

        /// <summary>
        ///     Gets the message of the log entry.
        /// </summary>
        [JsonProperty("message", Required = Required.Always, Order = 11)]
        public string Message { get; }
    }
}
