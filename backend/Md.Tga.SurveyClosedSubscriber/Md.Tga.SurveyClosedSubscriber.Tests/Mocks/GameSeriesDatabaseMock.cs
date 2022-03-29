namespace Md.Tga.SurveyClosedSubscriber.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.SurveyClosedSubscriber.Tests.Data;

    public class GameSeriesDatabaseMock : IGameSeriesReadOnlyDatabase
    {
        public Task<IGameSeries?> ReadByDocumentIdAsync(string documentId)
        {
            return Task.FromResult(TestData.CreateGameSeries() as IGameSeries);
        }

        public Task<IEnumerable<IGameSeries>> ReadManyAsync()
        {
            return null;
        }

        public Task<IEnumerable<IGameSeries>> ReadManyAsync(string fieldPath, object value)
        {
            return null;
        }

        public Task<IEnumerable<IGameSeries>> ReadManyAsync(string fieldPath, object value, OrderType orderType)
        {
            return null;
        }

        public Task<IGameSeries?> ReadOneAsync(string fieldPath, object value)
        {
            return null;
        }
    }
}
