namespace Md.Tga.StartGameSubscriber.Contracts
{
    using Md.GoogleCloud.Base.Logic;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the database configuration for collection games.
        /// </summary>
        DatabaseConfiguration Games { get; }

        /// <summary>
        ///     Gets the database configuration for collection game-series.
        /// </summary>
        DatabaseConfiguration GameSeries { get; }
    }
}
