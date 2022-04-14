namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    public class SaveGamePubSubClientMock : PubSubClientMock<ISaveGameMessage>, ISaveGamePubSubClient
    {
        public SaveGamePubSubClientMock()
        {
        }

        public SaveGamePubSubClientMock(ISaveGameMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override async Task<bool> CheckMessage(ISaveGameMessage expected, ISaveGameMessage actual)
        {
            await Task.CompletedTask;
            return true;
        }
    }
}
