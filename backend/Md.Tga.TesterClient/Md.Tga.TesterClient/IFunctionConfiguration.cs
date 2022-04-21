namespace Md.Tga.TesterClient
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the id of the document id of the test case.
        /// </summary>
        string DocumentId { get; }

        /// <summary>
        ///     Gets the name of the pub/sub topic.
        /// </summary>
        string SaveSurveyResultTopicName { get; }

        /// <summary>
        ///     Gets the name of the topic of the pub/sub message.
        /// </summary>
        string StartGameSeriesTopicName { get; }
    }
}
