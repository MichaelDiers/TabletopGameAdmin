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
        /// <param name="playerMappings">The mappings of a game.</param>
        [JsonConstructor]
        public SavePlayerMappingsMessage(string processId, PlayerMappings playerMappings)
            : this(processId, playerMappings as IPlayerMappings)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="SavePlayerMappingsMessage" />.
        /// </summary>
        /// <param name="processId">The global process id.</param>
        /// <param name="playerMappings">The mappings of a game.</param>
        public SavePlayerMappingsMessage(string processId, IPlayerMappings playerMappings)
            : base(processId)
        {
            this.PlayerMappings = playerMappings ?? throw new ArgumentNullException(nameof(playerMappings));
        }

        /// <summary>
        ///     Gets the player mappings of a game.
        /// </summary>
        [JsonProperty("playerMappings", Required = Required.Always, Order = 11)]
        public IPlayerMappings PlayerMappings { get; }
    }
}
