namespace Md.Tga.TesterClient.Model
{
    using Md.Tga.TesterClient.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the name of the database collection.
        /// </summary>
        public string CollectionName { get; set; } = "";

        /// <summary>
        ///     Gets the id of the document id of the test case.
        /// </summary>
        public string DocumentId { get; set; } = "";

        /// <summary>
        ///     Gets the project id.
        /// </summary>
        public string ProjectId { get; set; } = "";

        /// <summary>
        ///     Gets the name of the pub/sub message.
        /// </summary>
        public string PubSubMessageName { get; set; } = "";
    }
}
