namespace Md.Tga.Common.TestData.Mocks.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    public class GamesDatabaseMock : DatabaseMock<Game>, IGameDatabase
    {
        public GamesDatabaseMock()
            : this(Enumerable.Empty<Game>())
        {
        }

        public GamesDatabaseMock(Game game)
            : this(new[] {game})
        {
        }

        public GamesDatabaseMock(IEnumerable<Game> games)
            : base(
                new Dictionary<string, Game>(games.Select(g => new KeyValuePair<string, Game>(g.Id, g))),
                x => new KeyValuePair<string, Game>(Guid.NewGuid().ToString(), Game.FromDictionary(x.ToDictionary())))
        {
        }

        public override async Task<Game?> ReadOneAsync(string fieldPath, object value)
        {
            await Task.CompletedTask;
            if (fieldPath == Game.SurveyIdName)
            {
                return this.Values.FirstOrDefault(game => game.SurveyId == (string) value);
            }

            throw new NotImplementedException();
        }
    }
}
