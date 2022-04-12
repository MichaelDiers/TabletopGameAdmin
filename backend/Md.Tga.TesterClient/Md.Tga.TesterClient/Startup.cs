namespace Md.Tga.TesterClient
{
    using Google.Cloud.Functions.Hosting;
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Firestore.Logic;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Md.Tga.Common.PubSub.Logic;
    using Md.Tga.TesterClient.Contracts;
    using Md.Tga.TesterClient.Logic;
    using Md.Tga.TesterClient.Model;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Initialize the function.
    /// </summary>
    public class Startup : FunctionsStartup
    {
        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="context">The builder context.</param>
        /// <param name="services">Add services to this collection used in dependency injection context.</param>
        public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
        {
            var configuration = new FunctionConfiguration() as IFunctionConfiguration;
            context.Configuration.Bind(configuration);
            services.AddScoped(_ => configuration);

            services.AddScoped<IRuntimeEnvironment>(_ => configuration);
            services.AddScoped<ITestDataReadOnlyDatabase, TestDataReadOnlyDatabase>();

            services.AddScoped<IPubSubClientEnvironment>(_ => configuration);
            services.AddScoped<IStartGameSeriesPubSubClient, StartGameSeriesPubSubClient>();

            services.AddScoped<IFunctionProvider, FunctionProvider>();
        }
    }
}
