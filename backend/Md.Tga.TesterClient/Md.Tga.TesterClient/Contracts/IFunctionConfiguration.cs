namespace Md.Tga.TesterClient.Contracts
{
    using Md.Common.Contracts;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment, IPubSubClientEnvironment
    {
        /// <summary>
        ///     Gets the id of the document id of the test case.
        /// </summary>
        string DocumentId { get; }
    }
}
