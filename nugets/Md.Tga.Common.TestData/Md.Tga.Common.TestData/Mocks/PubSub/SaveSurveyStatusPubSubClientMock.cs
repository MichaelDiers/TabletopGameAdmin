namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class SaveSurveyStatusPubSubClientMock
        : PubSubClientMock<ISaveSurveyStatusMessage>, ISaveSurveyStatusPubSubClient
    {
        public SaveSurveyStatusPubSubClientMock()

        {
        }

        public SaveSurveyStatusPubSubClientMock(ISaveSurveyStatusMessage? expectedMessage)
            : base(expectedMessage)
        {
        }

        protected override async Task<bool> CheckMessage(
            ISaveSurveyStatusMessage expected,
            ISaveSurveyStatusMessage actual
        )
        {
            await Task.CompletedTask;
            return expected.CheckEqual(actual);
        }
    }
}
