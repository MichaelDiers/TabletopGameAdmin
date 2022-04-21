namespace Md.Tga.EvaluateGameTerminationSubscriber
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the topic name for creating emails.
        /// </summary>
        string CreateGameMailTopicName { get; }

        /// <summary>
        ///     Gets the topic name for saving the game status.
        /// </summary>
        string SaveGameStatusTopicName { get; }
    }
}
