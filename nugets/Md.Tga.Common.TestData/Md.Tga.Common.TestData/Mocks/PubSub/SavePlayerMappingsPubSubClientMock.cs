namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    public class SavePlayerMappingsPubSubClientMock
        : PubSubClientMock<ISavePlayerMappingsMessage>, ISavePlayerMappingsPubSubClient
    {
        public SavePlayerMappingsPubSubClientMock()
        {
        }

        public SavePlayerMappingsPubSubClientMock(ISavePlayerMappingsMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override async Task<bool> CheckMessage(
            ISavePlayerMappingsMessage expected,
            ISavePlayerMappingsMessage actual
        )
        {
            await Task.CompletedTask;
            return expected.CheckEqual(actual);
        }
    }
}
