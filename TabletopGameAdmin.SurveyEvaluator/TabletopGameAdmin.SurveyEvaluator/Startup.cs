namespace TabletopGameAdmin.SurveyEvaluator
{
	using Google.Cloud.Functions.Hosting;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///   Initialize the function.
	/// </summary>
	public class Startup : FunctionsStartup
	{
		/// <summary>
		///   This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services">Add services to this collection used in dependency injection context.</param>
		public void ConfigureServices(IServiceCollection services)
		{
		}
	}
}