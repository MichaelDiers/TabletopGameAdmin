namespace Md.Tga.TesterClient
{
    using Google.Cloud.Functions.Hosting;
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudPubSub.Model;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Firestore.Logic;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Md.Tga.Common.PubSub.Logic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Firestore.Models;
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
            var configuration = new FunctionConfiguration() as IFunctionConfiguration;
            context.Configuration.Bind(configuration);
            services.AddScoped(_ => configuration);

            services.AddScoped<IRuntimeEnvironment>(_ => configuration);
            services.AddScoped<ITestDataReadOnlyDatabase, TestDataReadOnlyDatabase>();
            services.AddScoped<IGameSeriesReadOnlyDatabase, GameSeriesReadOnlyDatabase>();
            services.AddScoped<IGameReadOnlyDatabase, GameReadOnlyDatabase>();
            services.AddScoped<ISurveyReadOnlyDatabase, SurveyReadOnlyDatabase>();
            services.AddScoped<IPlayerMappingsReadOnlyDatabase, PlayerMappingsReadOnlyDatabase>();

            services.AddScoped<IStartGameSeriesPubSubClient>(
                _ => new StartGameSeriesPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.StartGameSeriesTopicName)));
            services.AddScoped<ISaveSurveyResultPubSubClient>(
                _ => new SaveSurveyResultPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.SaveSurveyResultTopicName)));
            services.AddScoped<IStartGameTerminationPubSubClient>(
                _ => new StartGameTerminationPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.StartGameTerminationTopicName)));

            services.AddScoped<IFunctionProvider, FunctionProvider>();
        }
    }
}
