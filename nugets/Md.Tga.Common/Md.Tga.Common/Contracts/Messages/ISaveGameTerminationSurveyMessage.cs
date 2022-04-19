namespace Md.Tga.Common.Contracts.Messages
{
    using Md.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Describes a message for saving an <see cref="IGameTerminationSurvey" />.
    /// </summary>
    public interface ISaveGameTerminationSurveyMessage : IMessage
    {
        /// <summary>
        ///     Gets the game termination survey data.
        /// </summary>
        IGameTerminationSurvey GameTerminationSurvey { get; }
    }
}
