namespace Md.Tga.StartGameSubscriber.Model
{
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.StartGameSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the database configuration for collection games.
        /// </summary>
        public DatabaseConfiguration Games { get; set; }

        /// <summary>
        ///     Gets the database configuration for collection game-series.
        /// </summary>
        public DatabaseConfiguration GameSeries { get; set; }
    }
}
