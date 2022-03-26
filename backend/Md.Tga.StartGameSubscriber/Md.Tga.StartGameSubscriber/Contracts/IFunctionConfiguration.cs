namespace Md.Tga.StartGameSubscriber.Contracts
{
    using Md.Common.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the name of to topic initialize survey.
        /// </summary>
        string InitializeSurveyTopicName { get; }

        /// <summary>
        ///     Gets the name of to topic save game.
        /// </summary>
        string SaveGameTopicName { get; }

        /// <summary>
        ///     Gets the id of the translations document.
        /// </summary>
        string TranslationsDocument { get; }
    }
}
