namespace Md.Tga.Common.Messages
{
    using System.Collections.Generic;
    using System.Linq;
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
        /// <param name="gameMailType">The type of the email.</param>
        /// <param name="gameSeries">The <paramref name="game" /> is part of this game series.</param>
        /// <param name="game">The <paramref name="playerMappings" /> are part of this game.</param>
        /// <param name="playerMappings">The player mapping for the game.</param>
        /// <param name="gameTerminationResults">The game termination results.</param>
        [JsonConstructor]
        public CreateGameMailMessage(
            string processId,
            GameMailType gameMailType,
            GameSeries gameSeries,
            Game game,
            PlayerMappings playerMappings,
            IEnumerable<GameTerminationResult> gameTerminationResults
        )
            : this(
                processId,
                gameMailType,
                gameSeries,
                game,
                playerMappings,
                gameTerminationResults.Select(x => x as IGameTerminationResult))
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="CreateGameMailMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameMailType">The type of the email.</param>
        /// <param name="gameSeries">The <paramref name="game" /> is part of this game series.</param>
        /// <param name="game">The <paramref name="playerMappings" /> are part of this game.</param>
        /// <param name="playerMappings">The player mapping for the game.</param>
        /// <param name="gameTerminationResults">The game termination results.</param>
        public CreateGameMailMessage(
            string processId,
            GameMailType gameMailType,
            IGameSeries gameSeries,
            IGame game,
            IPlayerMappings playerMappings,
            IEnumerable<IGameTerminationResult> gameTerminationResults
        )
            : base(processId)
        {
            this.GameMailType = gameMailType;
            this.GameSeries = gameSeries;
            this.Game = game;
            this.PlayerMappings = playerMappings;
            this.GameTerminationResults = gameTerminationResults;
        }

        /// <summary>
        ///     Gets the game to be saved.
        /// </summary>
        [JsonProperty("game", Required = Required.Always, Order = 11)]
        public IGame Game { get; }

        /// <summary>
        ///     Gets the type of the email.
        /// </summary>
        [JsonProperty("gameMailType", Required = Required.Always, Order = 9)]
        public GameMailType GameMailType { get; }

        /// <summary>
        ///     The new game <see cref="Game" /> is linked to this <see cref="IGameSeries" />.
        /// </summary>
        [JsonProperty("gameSeries", Required = Required.Always, Order = 10)]
        public IGameSeries GameSeries { get; }

        /// <summary>
        ///     Gets the game termination results.
        /// </summary>
        [JsonProperty("gameTerminationResults", Required = Required.DisallowNull, Order = 13)]
        public IEnumerable<IGameTerminationResult> GameTerminationResults { get; }

        /// <summary>
        ///     Gets the player mappings for the game.
        /// </summary>
        [JsonProperty("playerMappings", Required = Required.Always, Order = 12)]
        public IPlayerMappings PlayerMappings { get; }
    }
}
