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
        [JsonConstructor]
        public SaveGameStatusMessage(string processId, GameStatus gameStatus)
            : this(processId, gameStatus as IGameStatus)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameStatusMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameStatus">The status of the game.</param>
        public SaveGameStatusMessage(string processId, IGameStatus gameStatus)
            : base(processId)
        {
            this.GameStatus = gameStatus;
        }

        /// <summary>
        ///     Gets the game status.
        /// </summary>
        public IGameStatus GameStatus { get; }
    }
}
