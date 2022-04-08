namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class SaveSurveyStatusPubSubClientMock : ISaveSurveyStatusPubSubClient
    {
        public async Task PublishAsync(ISaveSurveyStatusMessage message)
        {
            await Task.CompletedTask;
        }
    }
}
