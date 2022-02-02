namespace TabletopGameAdmin.SurveyEvaluator.Logic
{
	using System;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using TabletopGameAdmin.SurveyEvaluator.Contracts;
	using TabletopGameAdmin.SurveyEvaluator.Model;

	/// <summary>
	///   Provider that handles the business logic of the cloud function.
	/// </summary>
	public class FunctionProvider : IFunctionProvider
	{
		/// <summary>
		///   Handle an incoming json formatted message from google cloud pub/sub.
		/// </summary>
		/// <param name="json">The json message.</param>
		/// <returns>A <see cref="Task" /> without a result.</returns>
		public Task HandleAsync(string json)
		{
			var message = DeserializeObject(json);

			return Task.CompletedTask;
		}

		/// <summary>
		///   Deserialize json <see cref="string" /> to an <see cref="IMessage" />.
		/// </summary>
		/// <param name="json">The json formatted string.</param>
		/// <returns>An <see cref="Message" />.</returns>
		private static IMessage DeserializeObject(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				throw new ArgumentException("Value cannot be null or whitespace.", nameof(json));
			}

			var message = JsonConvert.DeserializeObject<Message>(json);
			if (message == null)
			{
				throw new ArgumentException($"Cannot deserialize object: {json}", nameof(json));
			}

			return message;
		}
	}
}