namespace Md.Tga.EvaluateGameTerminationSubscriber
{
    using Md.Common.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the topic name for creating emails.
        /// </summary>
        public string CreateGameMailTopicName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets the topic name for saving the game status.
        /// </summary>
        public string SaveGameStatusTopicName { get; set; } = string.Empty;
    }
}
