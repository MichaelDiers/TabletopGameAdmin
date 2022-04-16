namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class SaveSurveyResultPubSubClientMock
        : PubSubClientMock<ISaveSurveyResultMessage>, ISaveSurveyResultPubSubClient
    {
        public SaveSurveyResultPubSubClientMock()

        {
        }

        public SaveSurveyResultPubSubClientMock(ISaveSurveyResultMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override async Task<bool> CheckMessage(
            ISaveSurveyResultMessage expected,
            ISaveSurveyResultMessage actual
        )
        {
            await Task.CompletedTask;
            return true;
        }
    }
}
