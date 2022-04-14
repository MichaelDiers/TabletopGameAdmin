namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    public class StartSurveyPubSubClientMock : PubSubClientMock<IStartSurveyMessage>, IStartSurveyPubSubClient
    {
        public StartSurveyPubSubClientMock()
        {
        }

        public StartSurveyPubSubClientMock(IStartSurveyMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override async Task<bool> CheckMessage(IStartSurveyMessage expected, IStartSurveyMessage actual)
        {
            await Task.CompletedTask;
            return true;
        }
    }
}
