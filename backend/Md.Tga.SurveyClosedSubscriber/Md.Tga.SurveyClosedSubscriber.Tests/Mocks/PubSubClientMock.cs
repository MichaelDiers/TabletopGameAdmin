namespace Md.Tga.SurveyClosedSubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class PubSubClientMock : ISavePlayerMappingsPubSubClient, ISendMailPubSubClient
    {
        public Task PublishAsync(ISavePlayerMappingsMessage message)
        {
            return Task.CompletedTask;
        }

        public Task PublishAsync(ISendMailMessage message)
        {
            return Task.CompletedTask;
        }
    }
}
