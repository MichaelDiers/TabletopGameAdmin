namespace Md.Tga.Common.TestData.Mocks.Database
{
    using System.Collections.Generic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    public class GameConfigDatabaseMock : DatabaseMock<IGameConfig>, IGameConfigDatabase
    {
        public GameConfigDatabaseMock()
            : this(new Dictionary<string, IGameConfig>())
        {
        }

        public GameConfigDatabaseMock(IDictionary<string, IGameConfig> dictionary)
            : base(dictionary, GameConfig.FromDictionary)
        {
        }
    }
}
