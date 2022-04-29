namespace Md.Tga.MainSchedulerSubscriber
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : IFunctionProvider
    {
        /// <summary>
        ///     The method is executed for each incoming request.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result indicates termination.</returns>
        public Task HandleAsync()
        {
            return Task.CompletedTask;
        }
    }
}
