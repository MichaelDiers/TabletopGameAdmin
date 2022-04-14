namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class EvaluateSurveyPubSubClientMock : PubSubClientMock<IEvaluateSurveyMessage>, IEvaluateSurveyPubSubClient
    {
        public EvaluateSurveyPubSubClientMock()
        {
        }

        public EvaluateSurveyPubSubClientMock(IEvaluateSurveyMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override async Task<bool> CheckMessage(IEvaluateSurveyMessage expected, IEvaluateSurveyMessage actual)
        {
            await Task.CompletedTask;
            return true;
        }
    }
}
