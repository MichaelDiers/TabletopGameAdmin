namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    public class StartGamePubSubClientMock : PubSubClientMock<IStartGameMessage>, IStartGamePubSubClient
    {
        public StartGamePubSubClientMock()
        {
        }

        public StartGamePubSubClientMock(IStartGameMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override async Task<bool> CheckMessage(IStartGameMessage expected, IStartGameMessage actual)
        {
            await Task.CompletedTask;
            return expected.CheckEqual(actual);
        }
    }
}
