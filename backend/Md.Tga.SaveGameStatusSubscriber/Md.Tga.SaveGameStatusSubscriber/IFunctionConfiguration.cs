namespace Md.Tga.SaveGameStatusSubscriber
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets the topic name for creating mails.
        /// </summary>
        string CreateGameMailTopicName { get; }

        /// <summary>
        ///     Gets the topic name for starting a new game.
        /// </summary>
        string StartGameTopicName { get; }
    }
}
