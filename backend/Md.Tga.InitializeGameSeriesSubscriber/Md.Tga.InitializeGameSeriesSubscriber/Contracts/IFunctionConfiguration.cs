namespace Md.Tga.InitializeGameSeriesSubscriber.Contracts
{
    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the project id.
        /// </summary>
        string ProjectId { get; }

        /// <summary>
        ///     Gets the topic name for saving a game series.
        /// </summary>
        string SaveGameSeriesTopicName { get; }

        /// <summary>
        ///     Gets the topic name for starting a new game.
        /// </summary>
        string StartGameTopicName { get; }
    }
}
