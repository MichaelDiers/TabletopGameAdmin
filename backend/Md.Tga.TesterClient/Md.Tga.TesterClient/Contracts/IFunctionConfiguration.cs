namespace Md.Tga.TesterClient.Contracts
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudPubSub.Contracts.Model;

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
