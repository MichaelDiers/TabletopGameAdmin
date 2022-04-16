namespace Md.Tga.StartSurveySubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFunctions.Contracts.Logic;
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Firestore.Logic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Surveys.Common.PubSub.Contracts.Logic;
    using Surveys.Common.PubSub.Logic;

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
            var configuration = new FunctionConfiguration();
            context.Configuration.Bind(configuration);
            services.AddScoped<IFunctionConfiguration>(_ => configuration);

            services.AddScoped<IPubSubClientEnvironment>(_ => configuration);
            services.AddScoped<ISaveSurveyPubSubClient, SaveSurveyPubSubClient>();

            services.AddScoped<IRuntimeEnvironment>(_ => configuration);
            services.AddScoped<IPlayerMappingsReadOnlyDatabase, PlayerMappingsReadOnlyDatabase>();
            services.AddScoped<IGameReadOnlyDatabase, GameReadOnlyDatabase>();

            services.AddScoped<IPubSubProvider<IStartSurveyMessage>, FunctionProvider>();
        }
    }
}
