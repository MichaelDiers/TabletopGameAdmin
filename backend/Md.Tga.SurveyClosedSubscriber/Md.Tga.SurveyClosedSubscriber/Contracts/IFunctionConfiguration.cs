namespace Md.Tga.SurveyClosedSubscriber.Contracts
{
    using Md.Common.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the topic name for saving player mappings.
        /// </summary>
        string SavePlayerMappingsTopicName { get; }


        /// <summary>
        ///     Gets the topic name for sending emails.
        /// </summary>
        string SendMailTopicName { get; }
    }
}
