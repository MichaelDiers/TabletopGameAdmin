namespace Md.Tga.MainSchedulerSubscriber
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Describes the business logic of the cloud function.
    /// </summary>
    public interface IFunctionProvider
    {
        /// <summary>
        ///     The method is executed for each incoming request.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result indicates termination.</returns>
        Task HandleAsync();
    }
}
