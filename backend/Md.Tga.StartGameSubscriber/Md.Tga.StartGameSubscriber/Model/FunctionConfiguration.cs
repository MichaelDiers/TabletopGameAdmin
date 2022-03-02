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
        public string GamesCollectionName { get; set; }

        /// <summary>
        ///     Gets the name of the game-series collection.
        /// </summary>
        public string GameSeriesCollectionName { get; set; }

        /// <summary>
        ///     Gets the project id.
        /// </summary>
        public string ProjectId { get; set; }
    }
}
