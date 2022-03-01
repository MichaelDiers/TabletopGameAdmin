namespace Md.Tga.InitializeGameSeriesSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Logic;
    using Md.TabletopGameAdmin.Common.Contracts.Messages;
    using Md.Tga.InitializeGameSeriesSubscriber.Contracts;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IInitializeGameSeriesMessage, Function>
    {
        /// <summary>
        ///     Access the application settings.
        /// </summary>
        private readonly IFunctionConfiguration configuration;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="configuration">Access to the application settings.</param>
        public FunctionProvider(ILogger<Function> logger, IFunctionConfiguration configuration)
            : base(logger)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override Task HandleMessageAsync(IInitializeGameSeriesMessage message)
        {
            return Task.CompletedTask;
        }
    }
}
