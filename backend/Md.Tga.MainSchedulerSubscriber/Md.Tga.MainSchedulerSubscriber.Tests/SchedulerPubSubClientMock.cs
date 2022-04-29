namespace Md.Tga.MainSchedulerSubscriber.Tests
{
    using System.Threading.Tasks;
    using Md.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    public class SchedulerPubSubClientMock : ISchedulerPubSubClient
    {
        public Task PublishAsync(IMessage message)
        {
            return Task.CompletedTask;
        }
    }
}
