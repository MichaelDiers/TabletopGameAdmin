namespace Md.Tga.SaveGameTerminationSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloudFunctions.Contracts.Logic;
    using Md.GoogleCloudFunctions.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Google cloud function that handles Pub/Sub messages.
    /// </summary>
    [FunctionsStartup(typeof(Startup))]
    public class Function
        : PubSubFunction<SaveGameTerminationResultMessage, ISaveGameTerminationResultMessage, Function>
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Function" />.
        /// </summary>
        /// <param name="logger">The error logger.</param>
        /// <param name="provider">Provider for handling the business logic.</param>
        public Function(ILogger<Function> logger, IPubSubProvider<ISaveGameTerminationResultMessage> provider)
            : base(logger, provider)
        {
        }
    }
}
