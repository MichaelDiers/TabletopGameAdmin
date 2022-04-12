namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    public class StartGameSeriesPubSubClientMock
        : PubSubClientMock<IStartGameSeriesMessage>, IStartGameSeriesPubSubClient
    {
        public StartGameSeriesPubSubClientMock()
        {
        }

        public StartGameSeriesPubSubClientMock(IStartGameSeriesMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override Task<bool> CheckMessage(IStartGameSeriesMessage expected, IStartGameSeriesMessage actual)
        {
            return Task.FromResult(false);
        }
    }
}
