namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Describes a message that triggers a new survey.
    /// </summary>
    public interface IStartSurveyMessage : IMessage
    {
        /// <summary>
        ///     Gets the game for that a new survey is started.
        /// </summary>
        IGame Game { get; }

        /// <summary>
        ///     Gets the game series the <see cref="Game" /> is part of.
        /// </summary>
        IGameSeries GameSeries { get; }
    }
}
