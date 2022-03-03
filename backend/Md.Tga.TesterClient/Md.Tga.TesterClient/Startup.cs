namespace Md.Tga.TesterClient
{
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.TesterClient.Contracts;
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

            services.AddScoped<IDatabaseConfiguration>(
                _ => new DatabaseConfiguration(configuration.ProjectId, configuration.CollectionName));
            services.AddScoped<IReadOnlyDatabase, ReadonlyDatabase>();

            services.AddScoped<IPubSubClientConfiguration>(
                _ => new PubSubClientConfiguration(configuration.ProjectId, configuration.PubSubTopic));
            services.AddScoped<IPubSubClient, PubSubClient>();
        }
    }
}
