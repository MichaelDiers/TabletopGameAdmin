namespace Md.Tga.Common.TestData.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;

    public class GameSeriesDatabaseMock : DatabaseMock<IGameSeries>, IGameSeriesDatabase
    {
        public GameSeriesDatabaseMock()
            : this(Enumerable.Empty<IGameSeries>())
        {
        }

        public GameSeriesDatabaseMock(IEnumerable<IGameSeries> gameSeries)
            : base(
                new Dictionary<string, IGameSeries>(
                    gameSeries.Select(g => new KeyValuePair<string, IGameSeries>(g.Id, g))),
                x => new KeyValuePair<string, IGameSeries>(
                    Guid.NewGuid().ToString(),
                    GameSeries.FromDictionary(x.ToDictionary())))
        {
        }
    }
}
