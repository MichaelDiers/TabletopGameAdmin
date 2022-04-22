namespace Md.Tga.TesterClient
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public interface IFunctionProvider
    {
        /// <summary>
        ///     Read the test case from the database and publish the message to pub/sub to start the process.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result is a json formatted string.</returns>
        Task<string> InitializeGameSeries();
    }
}
