namespace Md.Tga.InitializeGameSeriesSubscriber.Model
{
    using Md.Tga.InitializeGameSeriesSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : IFunctionConfiguration
    {
        /// <summary>
        ///     Gets or sets the project id.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        ///     Gets or sets the topic name for saving a game series.
        /// </summary>
        public string SaveGameSeriesTopicName { get; set; }

        /// <summary>
        ///     Gets or sets the topic name for starting a new game.
        /// </summary>
        public string StartGameTopicName { get; set; }
    }
}
