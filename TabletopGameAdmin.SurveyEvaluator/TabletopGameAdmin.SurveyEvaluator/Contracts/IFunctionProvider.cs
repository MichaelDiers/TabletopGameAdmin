namespace TabletopGameAdmin.SurveyEvaluator.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Provider that handles the business logic of the cloud function.
	/// </summary>
	public interface IFunctionProvider
	{
		/// <summary>
		///   Handle an incoming json formatted message from google cloud pub/sub.
		/// </summary>
		/// <param name="json">The json message.</param>
		/// <returns>A <see cref="Task" /> without a result.</returns>
		Task HandleAsync(string json);
	}
}