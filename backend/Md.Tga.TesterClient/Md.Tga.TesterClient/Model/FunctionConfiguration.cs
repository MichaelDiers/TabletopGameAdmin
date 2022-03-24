namespace Md.Tga.TesterClient.Model
{
    using Md.Common.Model;
    using Md.Tga.TesterClient.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the id of the document id of the test case.
        /// </summary>
        public string DocumentId { get; set; } = "";

        /// <summary>
        ///     Gets the name of the topic of the pub/sub message.
        /// </summary>
        public string PubSubTopic { get; set; } = "";
    }
}
