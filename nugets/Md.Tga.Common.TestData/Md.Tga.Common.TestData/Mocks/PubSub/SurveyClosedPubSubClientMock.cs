namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class SurveyClosedPubSubClientMock : PubSubClientMock<ISurveyClosedMessage>, ISurveyClosedPubSubClient
    {
        public SurveyClosedPubSubClientMock()
        {
        }

        public SurveyClosedPubSubClientMock(ISurveyClosedMessage? expected)
            : base(expected)
        {
        }

        protected override async Task<bool> CheckMessage(ISurveyClosedMessage expected, ISurveyClosedMessage actual)
        {
            await Task.CompletedTask;
            return expected.CheckEqual(actual);
        }
    }
}
