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
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
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
            services.AddOptions<FunctionConfiguration>().Bind(context.Configuration).ValidateDataAnnotations();

            services.AddScoped<IRuntimeEnvironment>(
                provider => provider.GetService<IOptions<FunctionConfiguration>>().Value);

            services.AddScoped<ITestDataReadOnlyDatabase, TestDataReadOnlyDatabase>();
            services.AddScoped<IGameSeriesReadOnlyDatabase, GameSeriesReadOnlyDatabase>();
            services.AddScoped<IGameReadOnlyDatabase, GameReadOnlyDatabase>();
            services.AddScoped<ISurveyReadOnlyDatabase, SurveyReadOnlyDatabase>();
            services.AddScoped<IPlayerMappingsReadOnlyDatabase, PlayerMappingsReadOnlyDatabase>();
            services.AddScoped<ISurveyStatusReadOnlyDatabase, SurveyStatusReadOnlyDatabase>();
            services.AddScoped<IGameStatusReadOnlyDatabase, GameStatusReadOnlyDatabase>();

            services.AddScoped<IStartGameSeriesPubSubClient>(
                provider =>
                {
                    var config = provider.GetService<IOptions<FunctionConfiguration>>();
                    return new StartGameSeriesPubSubClient(
                        new PubSubClientEnvironment(
                            config.Value.Environment,
                            config.Value.ProjectId,
                            config.Value.StartGameSeriesTopicName));
                });

            services.AddScoped<ISaveSurveyResultPubSubClient>(
                provider =>
                {
                    var config = provider.GetService<IOptions<FunctionConfiguration>>();
                    return new SaveSurveyResultPubSubClient(
                        new PubSubClientEnvironment(
                            config.Value.Environment,
                            config.Value.ProjectId,
                            config.Value.SaveSurveyResultTopicName));
                });

            services.AddScoped<IStartGameTerminationPubSubClient>(
                provider =>
                {
                    var config = provider.GetService<IOptions<FunctionConfiguration>>();
                    return new StartGameTerminationPubSubClient(
                        new PubSubClientEnvironment(
                            config.Value.Environment,
                            config.Value.ProjectId,
                            config.Value.StartGameTerminationTopicName));
                });

            services.AddScoped<IFunctionProvider, FunctionProvider>();
        }
    }
}
