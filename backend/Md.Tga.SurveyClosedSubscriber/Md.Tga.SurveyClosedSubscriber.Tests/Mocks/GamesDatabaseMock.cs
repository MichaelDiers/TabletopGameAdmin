namespace Md.Tga.SurveyClosedSubscriber.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models;
    using Md.Tga.SurveyClosedSubscriber.Tests.Data;

    public class GamesDatabaseMock : IGameDatabase
    {
        public Task<string> InsertAsync(string documentId, IToDictionary data)
        {
            return null;
        }

        public Task<string> InsertAsync(IToDictionary data)
        {
            return null;
        }

        public Task UpdateByDocumentIdAsync(string documentId, IDictionary<string, object> updates)
        {
            return null;
        }

        public Task UpdateOneAsync(string fieldPath, object value, IDictionary<string, object> updates)
        {
            return null;
        }

        public Task<Game?> ReadByDocumentIdAsync(string documentId)
        {
            return null;
        }

        public Task<IEnumerable<Game>> ReadManyAsync()
        {
            return null;
        }

        public Task<IEnumerable<Game>> ReadManyAsync(string fieldPath, object value)
        {
            return null;
        }

        public Task<IEnumerable<Game>> ReadManyAsync(string fieldPath, object value, OrderType orderType)
        {
            return null;
        }

        public Task<Game?> ReadOneAsync(string fieldPath, object value)
        {
            return Task.FromResult(TestData.CreateGame());
        }
    }
}
