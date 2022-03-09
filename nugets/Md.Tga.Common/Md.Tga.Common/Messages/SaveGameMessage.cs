namespace Md.Tga.Common.Messages
{
    using System;
    using Md.GoogleCloud.Base.Messages;
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
        /// <param name="game">The game to be saved.</param>
        [JsonConstructor]
        public SaveGameMessage(string processId, Game game)
            : this(processId, game as IGame)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="game">The game to be saved.</param>
        public SaveGameMessage(string processId, IGame game)
            : base(processId)
        {
            this.Game = game ?? throw new ArgumentNullException(nameof(game));
        }

        /// <summary>
        ///     Gets the game to be saved.
        /// </summary>
        [JsonProperty("game", Required = Required.Always, Order = 11)]
        public IGame Game { get; }
    }
}
