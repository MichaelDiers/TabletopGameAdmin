namespace Md.Tga.SurveyClosedSubscriber.Tests.Mocks
{
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    public class SavePlayerMappingsPubSubClientMock : ISavePlayerMappingsPubSubClient
    {
        public Task PublishAsync(ISavePlayerMappingsMessage message)
        {
            return Task.CompletedTask;
        }
    }
}
