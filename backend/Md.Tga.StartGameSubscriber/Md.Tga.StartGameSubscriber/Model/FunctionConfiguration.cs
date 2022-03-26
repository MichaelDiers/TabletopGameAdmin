namespace Md.Tga.StartGameSubscriber.Model
{
    using Md.Common.Model;
    using Md.Tga.StartGameSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets or sets the name of to topic initialize survey.
        /// </summary>
        public string InitializeSurveyTopicName { get; set; } = "";

        /// <summary>
        ///     Gets the name of to topic save game.
        /// </summary>
        public string SaveGameTopicName { get; set; } = "";

        /// <summary>
        ///     Gets or sets the id of the translations document.
        /// </summary>
        public string TranslationsDocument { get; set; } = "";
    }
}
