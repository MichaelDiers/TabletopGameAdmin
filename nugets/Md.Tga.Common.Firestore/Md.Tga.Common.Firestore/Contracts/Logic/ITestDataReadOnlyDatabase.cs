namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Messages;

    /// <summary>
    ///     Read test data from database.
    /// </summary>
    public interface ITestDataReadOnlyDatabase
    {
        /// <summary>
        ///     Read test data for game series.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result is a <see cref="IStartGameSeriesMessage" />.</returns>
        Task<IStartGameSeriesMessage> ReadStartGameSeriesMessageAsync();
    }
}
