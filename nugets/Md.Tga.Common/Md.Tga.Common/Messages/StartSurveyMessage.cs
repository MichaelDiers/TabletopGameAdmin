namespace Md.Tga.Common.Messages
{
    using Md.Common.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a message that triggers a new survey.
    /// </summary>
    public class StartSurveyMessage : Message, IStartSurveyMessage
    {
        /// <summary>
        ///     Creates a new instance of <see cref="StartSurveyMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The <paramref name="game" /> is part of the this game series.</param>
        /// <param name="game">The game for that new survey is started.</param>
        [JsonConstructor]
        public StartSurveyMessage(string processId, GameSeries gameSeries, Game game)
            : this(processId, gameSeries, game as IGame)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="StartSurveyMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="gameSeries">The <paramref name="game" /> is part of the this game series.</param>
        /// <param name="game">The game for that new survey is started.</param>
        public StartSurveyMessage(string processId, IGameSeries gameSeries, IGame game)
            : base(processId)
        {
            this.Game = game;
            this.GameSeries = gameSeries;
        }

        /// <summary>
        ///     Gets the game for that a new survey is started.
        /// </summary>
        [JsonProperty("game", Required = Required.Always, Order = 11)]
        public IGame Game { get; }

        /// <summary>
        ///     Gets the game series the <see cref="Game" /> is part of.
        /// </summary>
        [JsonProperty("gameSeries", Required = Required.Always, Order = 12)]
        public IGameSeries GameSeries { get; }
    }
}
