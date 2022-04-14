namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class CreateMailPubSubClientMock : PubSubClientMock<ICreateMailMessage>, ICreateMailPubSubClient
    {
        public CreateMailPubSubClientMock()
        {
        }

        public CreateMailPubSubClientMock(ICreateMailMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override async Task<bool> CheckMessage(ICreateMailMessage expected, ICreateMailMessage actual)
        {
            await Task.CompletedTask;
            return true;
        }
    }
}
