namespace Md.Tga.TesterClient.Contracts
{
    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the name of the database collection.
        /// </summary>
        string CollectionName { get; }

        /// <summary>
        ///     Gets the id of the document id of the test case.
        /// </summary>
        string DocumentId { get; }

        /// <summary>
        ///     Gets the project id.
        /// </summary>
        string ProjectId { get; }

        /// <summary>
        ///     Gets the name of the topic of the pub/sub message.
        /// </summary>
        string PubSubTopic { get; }
    }
}
