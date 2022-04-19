namespace Md.Tga.Common.Messages
{
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a message for saving an <see cref="IGameTerminationSurveyResult" />.
    /// </summary>
    public class SaveGameTerminationSurveyResultMessage : Message, ISaveGameTerminationSurveyResultMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameTerminationSurveyResultMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameTerminationSurveyResult">The a result for a game termination survey.</param>
        [JsonConstructor]
        public SaveGameTerminationSurveyResultMessage(
            string processId,
            GameTerminationSurveyResult gameTerminationSurveyResult
        )
            : this(processId, gameTerminationSurveyResult as IGameTerminationSurveyResult)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameTerminationSurveyResultMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameTerminationSurveyResult">The a result for a game termination survey.</param>
        public SaveGameTerminationSurveyResultMessage(
            string processId,
            IGameTerminationSurveyResult gameTerminationSurveyResult
        )
            : base(processId)
        {
            this.GameTerminationSurveyResult = gameTerminationSurveyResult;
        }

        /// <summary>
        ///     Gets the game termination survey data.
        /// </summary>
        [JsonProperty("gameTerminationSurveyResult", Required = Required.Always, Order = 11)]
        public IGameTerminationSurveyResult GameTerminationSurveyResult { get; }
    }
}
