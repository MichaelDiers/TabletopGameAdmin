namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class SaveSurveyPubSubClientMock : PubSubClientMock<ISaveSurveyMessage>, ISaveSurveyPubSubClient
    {
        public SaveSurveyPubSubClientMock()

        {
        }

        public SaveSurveyPubSubClientMock(ISaveSurveyMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override async Task<bool> CheckMessage(ISaveSurveyMessage expected, ISaveSurveyMessage actual)
        {
            await Task.CompletedTask;
            return true;
        }
    }
}
