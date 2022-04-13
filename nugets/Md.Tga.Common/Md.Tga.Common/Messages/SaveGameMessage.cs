namespace Md.Tga.Common.Messages
{
    using System;
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Message that is sent to save a game.
    /// </summary>
    public class SaveGameMessage : Message, ISaveGameMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The new game <see paramref="game" /> is linked to this <see cref="IGameSeries" />.</param>
        /// <param name="game">The game to be saved.</param>
        [JsonConstructor]
        public SaveGameMessage(string processId, GameSeries gameSeries, Game game)
            : this(processId, gameSeries, game as IGame)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The new game <see paramref="game" /> is linked to this <see cref="IGameSeries" />.</param>
        /// <param name="game">The game to be saved.</param>
        public SaveGameMessage(string processId, IGameSeries gameSeries, IGame game)
            : base(processId)
        {
            this.Game = game ?? throw new ArgumentNullException(nameof(game));
            this.GameSeries = gameSeries ?? throw new ArgumentNullException(nameof(gameSeries));
        }

        /// <summary>
        ///     Gets the game to be saved.
        /// </summary>
        [JsonProperty("game", Required = Required.Always, Order = 11)]
        public IGame Game { get; }

        /// <summary>
        ///     The new game <see cref="Game" /> is linked to this <see cref="IGameSeries" />.
        /// </summary>
        [JsonProperty("gameSeries", Required = Required.Always, Order = 12)]
        public IGameSeries GameSeries { get; }
    }
}
