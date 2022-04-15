namespace Md.Tga.Common.Messages
{
    using System;
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a message for saving <see cref="IPlayerMappings" />.
    /// </summary>
    public class SavePlayerMappingsMessage : Message, ISavePlayerMappingsMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SavePlayerMappingsMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The
        ///     <param name="game"> is part of this game series.</param>
        /// </param>
        /// <param name="game">The <paramref name="playerMappings" /> are stored for this game.</param>
        /// <param name="playerMappings">The mappings of the game.</param>
        [JsonConstructor]
        public SavePlayerMappingsMessage(
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
        ///     Creates a new instance of <see cref="SavePlayerMappingsMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The
        ///     <param name="game"> is part of this game series.</param>
        /// </param>
        /// <param name="game">The <paramref name="playerMappings" /> are stored for this game.</param>
        /// <param name="playerMappings">The mappings of the game.</param>
        public SavePlayerMappingsMessage(
            string processId,
            IGameSeries gameSeries,
            IGame game,
            IPlayerMappings playerMappings
        )
            : base(processId)
        {
            this.GameSeries = gameSeries;
            this.Game = game;
            this.PlayerMappings = playerMappings ?? throw new ArgumentNullException(nameof(playerMappings));
        }

        /// <summary>
        ///     Gets the game.
        /// </summary>
        [JsonProperty("game", Required = Required.Always, Order = 12)]
        public IGame Game { get; }

        /// <summary>
        ///     Gets the game series.
        /// </summary>
        [JsonProperty("gameSeries", Required = Required.Always, Order = 11)]
        public IGameSeries GameSeries { get; }

        /// <summary>
        ///     Gets the player mappings of a game.
        /// </summary>
        [JsonProperty("playerMappings", Required = Required.Always, Order = 13)]
        public IPlayerMappings PlayerMappings { get; }
    }
}
