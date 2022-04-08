namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class SurveyClosedPubSubClientMock : ISurveyClosedPubSubClient
    {
        public async Task PublishAsync(ISurveyClosedMessage message)
        {
            await Task.CompletedTask;
        }
    }
}
