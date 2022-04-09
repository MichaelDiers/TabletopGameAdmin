namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class SendMailPubSubClientMock : PubSubClientMock<ISendMailMessage>, ISendMailPubSubClient
    {
        public SendMailPubSubClientMock()
        {
        }

        public SendMailPubSubClientMock(ISendMailMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override async Task<bool> CheckMessage(ISendMailMessage expected, ISendMailMessage actual)
        {
            await Task.CompletedTask;
            return expected.CheckEqual(actual);
        }
    }
}
