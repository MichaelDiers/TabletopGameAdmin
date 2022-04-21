namespace Md.Tga.Common.Messages
{
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a message for saving the status of a game.
    /// </summary>
    public class SaveGameStatusMessage : Message, ISaveGameStatusMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameStatusMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameStatus">The status of the game.</param>
        /// <param name="createGameMailMessage">An optional create mail message.</param>
        [JsonConstructor]
        public SaveGameStatusMessage(
            string processId,
            GameStatus gameStatus,
            CreateGameMailMessage? createGameMailMessage
        )
            : this(processId, gameStatus, createGameMailMessage as ICreateGameMailMessage)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameStatusMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameStatus">The status of the game.</param>
        /// <param name="createGameMailMessage">An optional create mail message.</param>
        public SaveGameStatusMessage(
            string processId,
            IGameStatus gameStatus,
            ICreateGameMailMessage? createGameMailMessage
        )
            : base(processId)
        {
            this.GameStatus = gameStatus;
            this.CreateGameMailMessage = createGameMailMessage;
        }

        /// <summary>
        ///     Gets an optional message that is created by the receiver of the <see cref="ISaveGameStatusMessage" />.
        /// </summary>
        [JsonProperty("createGameMailMessage", Required = Required.AllowNull, Order = 12)]
        public ICreateGameMailMessage? CreateGameMailMessage { get; }

        /// <summary>
        ///     Gets the game status.
        /// </summary>
        [JsonProperty("gameStatus", Required = Required.Always, Order = 11)]
        public IGameStatus GameStatus { get; }
    }
}
