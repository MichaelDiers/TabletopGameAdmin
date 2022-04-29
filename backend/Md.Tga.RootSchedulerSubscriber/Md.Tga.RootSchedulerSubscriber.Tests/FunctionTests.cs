namespace Md.Tga.RootSchedulerSubscriber.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Function" />.
    /// </summary>
    public class FunctionTests
    {
        [Fact]
        public async Task HandleAsync()
        {
            var cloudEvent = new CloudEvent
            {
                Type = MessagePublishedData.MessagePublishedCloudEventType,
                Source = new Uri("//pubsub.googleapis.com", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow
            };

            var logger = new MemoryLogger<Function>();
            var provider =
                new FunctionProvider(new[] {new SchedulerPubSubClientMock(), new SchedulerPubSubClientMock()});
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}
