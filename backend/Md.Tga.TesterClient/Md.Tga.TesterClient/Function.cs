namespace Md.Tga.TesterClient
{
    using System.Net;
    using System.Threading.Tasks;
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloudFunctions.Logic;
    using Md.Tga.TesterClient.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Google cloud function that handles Pub/Sub messages.
    /// </summary>
    [FunctionsStartup(typeof(Startup))]
    public class Function : HttpFunction<Function>
    {
        /// <summary>
        ///     Handles the business logic.
        /// </summary>
        private readonly IFunctionProvider functionProvider;

        /// <summary>
        ///     Creates a new instance of <see cref="Function" />.
        /// </summary>
        /// <param name="logger">The error logger.</param>
        /// <param name="functionProvider">Handles the business logic.</param>
        public Function(ILogger<Function> logger, IFunctionProvider functionProvider)
            : base(logger)
        {
            this.functionProvider = functionProvider;
        }

        /// <summary>
        ///     Asynchronously handles the get request in <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Request" />,
        ///     populating <see cref="P:Microsoft.AspNetCore.Http.HttpContext.Response" /> to indicate the result.
        /// </summary>
        /// <param name="context">The HTTP context containing the request and response.</param>
        /// <param name="content">The content of the request.</param>
        /// <returns>A task to indicate when the request is complete.</returns>
        protected override async Task HandleGetAsync(HttpContext context, object? content)
        {
            await this.functionProvider.InitializeGameSeries();
            await this.SetJsonResponse(context, HttpStatusCode.OK, "{ test: 'ok' }");
        }
    }
}
