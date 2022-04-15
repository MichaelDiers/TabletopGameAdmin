namespace Md.Tga.Common.Messages
{
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Message that is sent for creating an email.
    /// </summary>
    public class CreateGameMailMessage : Message, ICreateGameMailMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="CreateGameMailMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The <paramref name="game" /> is part of this game series.</param>
        /// <param name="game">The <paramref name="playerMappings" /> are part of this game.</param>
        /// <param name="playerMappings">The player mapping for the game.</param>
        [JsonConstructor]
        public CreateGameMailMessage(
            string processId,
            GameSeries gameSeries,
            Game game,
            PlayerMappings playerMappings
        )
            : this(
                processId,
                gameSeries,
                game,
                playerMappings as IPlayerMappings)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="CreateGameMailMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The <paramref name="game" /> is part of this game series.</param>
        /// <param name="game">The <paramref name="playerMappings" /> are part of this game.</param>
        /// <param name="playerMappings">The player mapping for the game.</param>
        public CreateGameMailMessage(
            string processId,
            IGameSeries gameSeries,
            IGame game,
            IPlayerMappings playerMappings
        )
            : base(processId)
        {
            this.GameSeries = gameSeries;
            this.Game = game;
            this.PlayerMappings = playerMappings;
        }

        /// <summary>
        ///     Gets the game to be saved.
        /// </summary>
        [JsonProperty("game", Required = Required.Always, Order = 11)]
        public IGame Game { get; }

        /// <summary>
        ///     The new game <see cref="Game" /> is linked to this <see cref="IGameSeries" />.
        /// </summary>
        [JsonProperty("gameSeries", Required = Required.Always, Order = 10)]
        public IGameSeries GameSeries { get; }

        /// <summary>
        ///     Gets the player mappings for the game.
        /// </summary>
        [JsonProperty("playerMappings", Required = Required.Always, Order = 12)]
        public IPlayerMappings PlayerMappings { get; }
    }
}
