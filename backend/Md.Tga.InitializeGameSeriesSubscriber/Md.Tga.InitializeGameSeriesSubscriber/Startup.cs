namespace Md.Tga.InitializeGameSeriesSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.TabletopGameAdmin.Common.Contracts.Messages;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Md.Tga.InitializeGameSeriesSubscriber.Contracts;
    using Md.Tga.InitializeGameSeriesSubscriber.Logic;
    using Md.Tga.InitializeGameSeriesSubscriber.Model;

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

            services.AddScoped<IPubSubProvider<IInitializeGameSeriesMessage>, FunctionProvider>();
        }
    }
}
