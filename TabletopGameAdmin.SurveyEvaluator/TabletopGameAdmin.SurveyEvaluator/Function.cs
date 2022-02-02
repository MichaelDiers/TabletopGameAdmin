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
		///   Creates a new instance of <see cref="Function" />.
		/// </summary>
		/// <param name="logger">The error logger.</param>
		public Function(ILogger<Function> logger)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <summary>
		///   Handle incoming messages from google cloud pub/sub.
		/// </summary>
		/// <param name="cloudEvent">The raised event.</param>
		/// <param name="data">The data send with the message.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken" />.</param>
		/// <returns>A <see cref="Task" /> without a result.</returns>
		public Task HandleAsync(CloudEvent cloudEvent, MessagePublishedData data, CancellationToken cancellationToken)
		{
			try
			{
				// do something
			}
			catch (Exception e)
			{
				this.logger.LogError(e, "Unexpected error!");
			}

			return Task.CompletedTask;
		}
	}
}