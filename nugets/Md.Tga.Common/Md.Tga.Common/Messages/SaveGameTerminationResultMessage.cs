namespace Md.Tga.Common.Messages
{
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a message for saving an <see cref="IGameTerminationResult" />.
    /// </summary>
    public class SaveGameTerminationResultMessage : Message, ISaveGameTerminationResultMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameTerminationResultMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameTerminationResult">The a result for a game termination survey.</param>
        [JsonConstructor]
        public SaveGameTerminationResultMessage(string processId, GameTerminationResult gameTerminationResult)
            : this(processId, gameTerminationResult as IGameTerminationResult)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameTerminationResultMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameTerminationResult">The a result for a game termination survey.</param>
        public SaveGameTerminationResultMessage(string processId, IGameTerminationResult gameTerminationResult)
            : base(processId)
        {
            this.GameTerminationResult = gameTerminationResult;
        }

        /// <summary>
        ///     Gets the game termination survey data.
        /// </summary>
        [JsonProperty("gameTerminationResult", Required = Required.Always, Order = 11)]
        public IGameTerminationResult GameTerminationResult { get; }
    }
}
