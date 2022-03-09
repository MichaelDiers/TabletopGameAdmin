namespace Md.Tga.StartGameSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.StartGameSubscriber.Contracts;
    using Md.Tga.StartGameSubscriber.Logic;
    using Md.Tga.StartGameSubscriber.Model;
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
            var configuration = new FunctionConfiguration();
            context.Configuration.Bind(configuration);

            services.AddScoped<IFunctionConfiguration>(_ => configuration);

            services.AddScoped<IGameSeriesReadOnlyDatabase>(
                _ => new GameSeriesReadOnlyDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.GameSeriesCollectionName)));
            services.AddScoped<IGamesReadOnlyDatabase>(
                _ => new GamesReadOnlyDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.GamesCollectionName)));
            services.AddScoped<ITranslationsReadOnlyDatabase>(
                _ => new TranslationsReadOnlyDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.TranslationsCollectionName),
                    configuration));

            services.AddScoped<IInitializeSurveyPubSubClient>(
                _ => new InitializeSurveyPubSubClient(
                    new PubSubClientConfiguration(configuration.ProjectId, configuration.InitializeSurveyTopicName)));
            services.AddScoped<ISaveGamePubSubClient>(
                _ => new SaveGamePubSubClient(
                    new PubSubClientConfiguration(configuration.ProjectId, configuration.SaveGameTopicName)));

            services.AddScoped<IPubSubProvider<IStartGameMessage>, FunctionProvider>();
        }
    }
}
