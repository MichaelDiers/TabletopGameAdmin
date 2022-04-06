namespace Md.Tga.SurveyClosedSubscriber.Model
{
    using Md.Common.Model;
    using Md.Tga.SurveyClosedSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the topic name for saving player mappings.
        /// </summary>
        public string SavePlayerMappingsTopicName { get; set; }
    }
}
