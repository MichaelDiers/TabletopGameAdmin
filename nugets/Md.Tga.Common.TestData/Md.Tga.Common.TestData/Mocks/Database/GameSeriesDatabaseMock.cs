namespace Md.Tga.Common.TestData.Mocks.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    public class GameSeriesDatabaseMock : DatabaseMock<IGameSeries>, IGameSeriesDatabase
    {
        public GameSeriesDatabaseMock(IGameSeries gameSeries, IGame game)
            : this(new Dictionary<string, IGameSeries> {{game.InternalGameSeriesId, gameSeries}})
        {
        }

        public GameSeriesDatabaseMock()
            : this(new Dictionary<string, IGameSeries>())
        {
        }

        public GameSeriesDatabaseMock(IEnumerable<IGameSeries> gameSeries)
            : this(
                new Dictionary<string, IGameSeries>(
                    gameSeries.Select(g => new KeyValuePair<string, IGameSeries>(g.Id, g))))
        {
        }

        public GameSeriesDatabaseMock(IDictionary<string, IGameSeries> dictionary)
            : base(
                dictionary,
                x => new KeyValuePair<string, IGameSeries>(
                    Guid.NewGuid().ToString(),
                    GameSeries.FromDictionary(x.ToDictionary())))
        {
        }
    }
}
