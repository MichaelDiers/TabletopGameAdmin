namespace Md.Tga.StartGameSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.Common.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Firestore.Logic;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Md.Tga.Common.PubSub.Logic;
    using Md.Tga.StartGameSubscriber.Contracts;
    using Md.Tga.StartGameSubscriber.Logic;
    using Md.Tga.StartGameSubscriber.Model;
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

            services.AddScoped<IRuntimeEnvironment>(_ => configuration);
            services.AddScoped<IGameReadOnlyDatabase, GameReadOnlyDatabase>();
            services.AddScoped<IGameSeriesReadOnlyDatabase, GameSeriesReadOnlyDatabase>();
            services.AddScoped<ITranslationsReadOnlyDatabase, TranslationsReadOnlyDatabase>();

            services.AddScoped<ISaveGamePubSubClient>(
                _ => new SaveGamePubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.SaveGameTopicName)));
            services.AddScoped<IInitializeSurveyPubSubClient>(
                _ => new InitializeSurveyPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.InitializeSurveyTopicName)));

            services.AddScoped<IPubSubProvider<IStartGameMessage>, FunctionProvider>();
        }
    }
}
