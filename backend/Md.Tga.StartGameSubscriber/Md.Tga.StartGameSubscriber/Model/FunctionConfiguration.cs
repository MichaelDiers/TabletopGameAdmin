namespace Md.Tga.StartGameSubscriber.Model
{
    using Md.Tga.StartGameSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the name of the games collection.
        /// </summary>
        public string GamesCollectionName { get; set; } = "";

        /// <summary>
        ///     Gets the name of the game-series collection.
        /// </summary>
        public string GameSeriesCollectionName { get; set; } = "";

        /// <summary>
        ///     Gets or sets the name of to topic initialize survey.
        /// </summary>
        public string InitializeSurveyTopicName { get; set; } = "";

        /// <summary>
        ///     Gets the project id.
        /// </summary>
        public string ProjectId { get; set; } = "";

        /// <summary>
        ///     Gets or sets the name of the translations collection.
        /// </summary>
        public string TranslationsCollectionName { get; set; } = "";

        /// <summary>
        ///     Gets or sets the id of the translations document.
        /// </summary>
        public string TranslationsDocument { get; set; } = "";
    }
}
