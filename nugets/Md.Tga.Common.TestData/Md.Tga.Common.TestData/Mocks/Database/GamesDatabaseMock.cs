namespace Md.Tga.Common.TestData.Mocks.Database
{
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
                Game.FromDictionary)
        {
        }

        public async Task<int> CountGames(string gameSeriesDocumentId)
        {
            await Task.CompletedTask;
            return this.Values.Count(game => game.ParentDocumentId == gameSeriesDocumentId);
        }
    }
}
