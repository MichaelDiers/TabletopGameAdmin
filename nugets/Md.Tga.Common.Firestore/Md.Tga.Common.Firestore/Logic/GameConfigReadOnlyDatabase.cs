namespace Md.Tga.Common.Firestore.Logic
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Model;
    using Md.Common.Extensions;
    using Md.Common.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     ReadOnly database for <see cref="GameConfig" /> collections,
    /// </summary>
    public class GameConfigReadOnlyDatabase : ReadonlyDatabase<IGameConfig>, IGameConfigReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the database collection.
        /// </summary>
        public const string CollectionName = "game-configs";

        /// <summary>
        ///     Creates a new instance of <see cref="GameReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public GameConfigReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, GameConfigReadOnlyDatabase.CollectionName, GameConfigReadOnlyDatabase.Convert)
        {
        }

        /// <summary>
        ///     Create a new <see cref="GameConfig" />.
        /// </summary>
        /// <param name="dictionary">The data of the config.</param>
        /// <returns>An <see cref="IGameConfig" />.</returns>
        private static IGameConfig Convert(IDictionary<string, object> dictionary)
        {
            var json = dictionary.GetString("json");
            return Serializer.DeserializeObject<GameConfig>(json);
        }
    }
}
