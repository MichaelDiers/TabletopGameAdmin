namespace Md.Tga.StartGameSubscriber.Contracts
{
    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the name of the games collection.
        /// </summary>
        string GamesCollectionName { get; }

        /// <summary>
        ///     Gets the name of the game-series collection.
        /// </summary>
        string GameSeriesCollectionName { get; }

        /// <summary>
        ///     Gets the project id.
        /// </summary>
        string ProjectId { get; }
    }
}
