namespace Md.Tga.RootSchedulerSubscriber
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Framework;
    using Google.Cloud.Functions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Google cloud function that handles Pub/Sub messages.
    /// </summary>
    [FunctionsStartup(typeof(Startup))]
    public class Function : ICloudEventFunction
    {
        /// <summary>
        ///     A business logic handler.
        /// </summary>
        private readonly IFunctionProvider functionProvider;

        /// <summary>
        ///     Log errors to the google cloud logs.
        /// </summary>
        private readonly ILogger<Function> logger;

        /// <summary>
        ///     Creates a new instance of <see cref="Function" />.
        /// </summary>
        /// <param name="logger">The error logger.</param>
        /// <param name="functionProvider">Provider for handling the business logic.</param>
        public Function(ILogger<Function> logger, IFunctionProvider functionProvider)
        {
            this.logger = logger;
            this.functionProvider = functionProvider;
        }

        /// <summary>Asynchronously handles the specified CloudEvent.</summary>
        /// <param name="cloudEvent">The CloudEvent extracted from the request.</param>
        /// <param name="cancellationToken">A cancellation token which indicates if the request is aborted.</param>
        /// <returns>
        ///     A task representing the potentially-asynchronous handling of the event.
        ///     If the task completes, the function is deemed to be successful.
        /// </returns>
        public async Task HandleAsync(CloudEvent cloudEvent, CancellationToken cancellationToken)
        {
            try
            {
                await this.functionProvider.HandleAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Unhandled exception occurred");
            }
        }
    }
}
