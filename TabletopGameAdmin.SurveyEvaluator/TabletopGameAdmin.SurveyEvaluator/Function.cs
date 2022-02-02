namespace TabletopGameAdmin.SurveyEvaluator
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using CloudNative.CloudEvents;
	using Google.Cloud.Functions.Framework;
	using Google.Cloud.Functions.Hosting;
	using Google.Events.Protobuf.Cloud.PubSub.V1;
	using Microsoft.Extensions.Logging;
	using TabletopGameAdmin.SurveyEvaluator.Contracts;

	/// <summary>
	///   Google cloud function that handles Pub/Sub messages.
	/// </summary>
	[FunctionsStartup(typeof(Startup))]
	public class Function : ICloudEventFunction<MessagePublishedData>
	{
		/// <summary>
		///   The error logger writes to the google cloud..
		/// </summary>
		private readonly ILogger<Function> logger;

		/// <summary>
		///   Provider for handling the business logic.
		/// </summary>
		private readonly IFunctionProvider provider;

		/// <summary>
		///   Creates a new instance of <see cref="Function" />.
		/// </summary>
		/// <param name="logger">The error logger.</param>
		/// <param name="provider">Provider for handling the business logic.</param>
		public Function(ILogger<Function> logger, IFunctionProvider provider)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
		}

		/// <summary>
		///   Handle incoming messages from google cloud pub/sub.
		/// </summary>
		/// <param name="cloudEvent">The raised event.</param>
		/// <param name="data">The data send with the message.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken" />.</param>
		/// <returns>A <see cref="Task" /> without a result.</returns>
		public async Task HandleAsync(CloudEvent cloudEvent, MessagePublishedData data, CancellationToken cancellationToken)
		{
			try
			{
				await this.provider.HandleAsync(data?.Message?.TextData);
			}
			catch (Exception e)
			{
				this.logger.LogError(e, "Unexpected error!");
			}
		}
	}
}