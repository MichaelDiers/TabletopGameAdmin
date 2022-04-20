namespace Md.Tga.StartGameTerminationSubscriber
{
    using Md.Common.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the name of the pub/sub topic.
        /// </summary>
        public string TopicName { get; set; } = string.Empty;
    }
}
