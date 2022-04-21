namespace Md.Tga.TesterClient
{
    using Md.Common.Model;

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
        ///     Gets the name of the pub/sub topic.
        /// </summary>
        public string SaveSurveyResultTopicName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets the name of the topic of the pub/sub message.
        /// </summary>
        public string StartGameSeriesTopicName { get; set; } = "";
    }
}
