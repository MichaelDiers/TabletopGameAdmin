namespace Md.Tga.InitializeGameSeriesSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;
    using Md.TabletopGameAdmin.Common.Contracts.Messages;
    using Md.TabletopGameAdmin.Common.Messages;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Google cloud function that handles Pub/Sub messages.
    /// </summary>
    [FunctionsStartup(typeof(Startup))]
    public class Function : PubSubFunction<InitializeGameSeriesMessage, IInitializeGameSeriesMessage, Function>
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Function" />.
        /// </summary>
        /// <param name="logger">The error logger.</param>
        /// <param name="provider">Provider for handling the business logic.</param>
        public Function(ILogger<Function> logger, IPubSubProvider<IInitializeGameSeriesMessage> provider)
            : base(logger, provider)
        {
        }
    }
}
