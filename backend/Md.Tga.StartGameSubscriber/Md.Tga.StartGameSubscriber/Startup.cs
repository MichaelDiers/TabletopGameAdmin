namespace Md.Tga.StartGameSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloud.Base.Contracts.Logic;
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
                _ => new GameSeriesReadOnlyDatabase(configuration.GameSeries));
            services.AddScoped<IGamesReadOnlyDatabase>(_ => new GamesReadOnlyDatabase(configuration.Games));
            services.AddScoped<IPubSubProvider<IStartGameMessage>, FunctionProvider>();
        }
    }
}
