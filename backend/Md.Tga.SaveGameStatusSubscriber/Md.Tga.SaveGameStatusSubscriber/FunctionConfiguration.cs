namespace Md.Tga.SaveGameStatusSubscriber
{
    using Md.Common.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the topic name for creating mails.
        /// </summary>
        public string CreateGameMailTopicName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets the topic name for starting a new game.
        /// </summary>
        public string StartGameTopicName { get; set; } = string.Empty;
    }
}
