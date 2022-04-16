namespace Md.Tga.Common.Messages
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.Common.Logic;
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
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

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add("message", this.Message);
            dictionary.Add(
                "exception",
                this.Exception != null ? Serializer.SerializeObject(this.Exception) : string.Empty);
            dictionary.Add("processId", this.ProcessId);
            return dictionary;
        }

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <returns>An <see cref="IDictionary{TKey,TValue}" />.</returns>
        public IDictionary<string, object> ToDictionary()
        {
            return this.AddToDictionary(new Dictionary<string, object>());
        }

        /// <summary>
        ///     Initialize the object from dictionary data.
        /// </summary>
        /// <param name="dictionary">The object is initialized from the dictionary.</param>
        /// <returns>An instance of <see cref="ILogMessage" />.</returns>
        public static ILogMessage FromDictionary(IDictionary<string, object> dictionary)
        {
            var message = dictionary.GetString("message");
            var exception = dictionary.GetString("exception");
            var processId = dictionary.GetString("processId");

            return new LogMessage(
                processId,
                message,
                string.IsNullOrWhiteSpace(exception) ? null : Serializer.DeserializeObject<Exception>(exception));
        }
    }
}
