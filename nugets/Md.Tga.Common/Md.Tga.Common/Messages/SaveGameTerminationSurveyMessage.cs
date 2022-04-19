namespace Md.Tga.Common.Messages
{
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a message for saving an <see cref="IGameTerminationSurvey" />.
    /// </summary>
    public class SaveGameTerminationSurveyMessage : Message, ISaveGameTerminationSurveyMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameTerminationSurveyMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameTerminationSurvey">The game termination survey data.</param>
        [JsonConstructor]
        public SaveGameTerminationSurveyMessage(string processId, GameTerminationSurvey gameTerminationSurvey)
            : this(processId, gameTerminationSurvey as IGameTerminationSurvey)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameTerminationSurveyMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameTerminationSurvey">The game termination survey data.</param>
        public SaveGameTerminationSurveyMessage(string processId, IGameTerminationSurvey gameTerminationSurvey)
            : base(processId)
        {
            this.GameTerminationSurvey = gameTerminationSurvey;
        }

        /// <summary>
        ///     Gets the game termination survey data.
        /// </summary>
        [JsonProperty("gameTerminationSurvey", Required = Required.Always, Order = 11)]
        public IGameTerminationSurvey GameTerminationSurvey { get; }
    }
}
