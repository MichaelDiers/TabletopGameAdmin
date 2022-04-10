namespace Md.Tga.Common.Models
{
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.Common.Model;
    using Md.Tga.Common.Contracts.Models;
    using Newtonsoft.Json;

    /// <summary>
    ///     Player and termination link mapping.
    /// </summary>
    public class GameTermination : ToDictionaryConverter, IGameTermination
    {
        /// <summary>
        ///     The json name for the <see cref="PlayerId" />.
        /// </summary>
        public const string PlayerIdName = "playerId";

        /// <summary>
        ///     The json name for the <see cref="TerminationId" />.
        /// </summary>
        public const string TerminationIdName = "terminationId";

        /// <summary>
        ///     Creates a new instance of <see cref="GameTermination" />.
        /// </summary>
        /// <param name="playerId">The id of the player.</param>
        /// <param name="terminationId">The id for terminating the game.</param>
        public GameTermination(string playerId, string terminationId)
        {
            this.PlayerId = playerId.ValidateIsAGuid(nameof(playerId));
            this.TerminationId = terminationId.ValidateIsAGuid(nameof(terminationId));
        }

        /// <summary>
        ///     Gets the id of the player.
        /// </summary>
        [JsonProperty(GameTermination.PlayerIdName, Required = Required.Always, Order = 11)]
        public string PlayerId { get; }

        /// <summary>
        ///     Gets the id for game termination.
        /// </summary>
        [JsonProperty(GameTermination.TerminationIdName, Required = Required.Always, Order = 12)]
        public string TerminationId { get; }

        /// <summary>
        ///     Add the properties of the object to the dictionary.
        /// </summary>
        /// <param name="dictionary">Values are added to this dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(GameTermination.PlayerIdName, this.PlayerId);
            dictionary.Add(GameTermination.TerminationIdName, this.TerminationId);
            return dictionary;
        }

        /// <summary>
        ///     Creates a new instance of <see cref="GameTermination" /> from the given dictionary values.
        /// </summary>
        /// <param name="dictionary">The values of the object.</param>
        /// <returns>An <see cref="IGameTermination" />.</returns>
        public static IGameTermination FromDictionary(IDictionary<string, object> dictionary)
        {
            var playerId = dictionary.GetString(GameTermination.PlayerIdName);
            var terminationId = dictionary.GetString(GameTermination.TerminationIdName);
            return new GameTermination(playerId, terminationId);
        }
    }
}
