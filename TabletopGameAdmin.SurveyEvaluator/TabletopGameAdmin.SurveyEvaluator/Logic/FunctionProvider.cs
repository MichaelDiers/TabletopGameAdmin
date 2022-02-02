namespace TabletopGameAdmin.SurveyEvaluator.Logic
{
	using System.Threading.Tasks;
	using TabletopGameAdmin.SurveyEvaluator.Contracts;

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
			return Task.CompletedTask;
		}
	}
}