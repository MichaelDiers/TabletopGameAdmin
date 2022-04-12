namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    public class SaveGameSeriesPubSubClientMock : PubSubClientMock<ISaveGameSeriesMessage>, ISaveGameSeriesPubSubClient
    {
        public SaveGameSeriesPubSubClientMock()
        {
        }

        public SaveGameSeriesPubSubClientMock(ISaveGameSeriesMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override async Task<bool> CheckMessage(ISaveGameSeriesMessage expected, ISaveGameSeriesMessage actual)
        {
            await Task.CompletedTask;
            return expected.CheckEqual(actual);
        }
    }
}
