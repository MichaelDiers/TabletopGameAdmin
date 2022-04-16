namespace Md.Tga.Common.TestData.Mocks.Database
{
    using System.Collections.Generic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    public class PlayerMappingsDatabaseMock : DatabaseMock<IPlayerMappings>, IPlayerMappingsDatabase
    {
        public PlayerMappingsDatabaseMock()
            : this(new Dictionary<string, IPlayerMappings>())
        {
        }

        public PlayerMappingsDatabaseMock(IDictionary<string, IPlayerMappings> dictionary)
            : base(dictionary, PlayerMappings.FromDictionary)

        {
        }
    }
}
