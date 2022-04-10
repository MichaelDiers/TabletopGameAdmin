namespace Md.Tga.Common.TestData.Mocks.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    public class GamesDatabaseMock : DatabaseMock<IGame>, IGameDatabase
    {
        public GamesDatabaseMock()
            : this(Enumerable.Empty<IGame>())
        {
        }

        public GamesDatabaseMock(IGame game)
            : this(new[] {game})
        {
        }

        public GamesDatabaseMock(IEnumerable<IGame> games)
            : base(
                new Dictionary<string, IGame>(games.Select(g => new KeyValuePair<string, IGame>(g.DocumentId, g))),
                x => new KeyValuePair<string, IGame>(Guid.NewGuid().ToString(), Game.FromDictionary(x.ToDictionary())))
        {
        }

        public override async Task<IGame?> ReadOneAsync(string fieldPath, object value)
        {
            await Task.CompletedTask;
            if (fieldPath == Game.SurveyDocumentIdName)
            {
                return this.Values.FirstOrDefault(game => game.SurveyDocumentId == (string) value);
            }

            throw new NotImplementedException();
        }
    }
}
