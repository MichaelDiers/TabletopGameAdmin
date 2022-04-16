namespace Md.Tga.StartSurveySubscriber.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.TestData.Generators;
    using Md.Tga.Common.TestData.Mocks.Database;
    using Md.Tga.Common.TestData.Mocks.PubSub;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Function" />.
    /// </summary>
    public class FunctionTests
    {
        [Fact]
        public async void HandleAsync()
        {
            var container = new TestDataContainer();
            var message = new StartSurveyMessage(Guid.NewGuid().ToString(), container.GameSeries, container.Game);
            await FunctionTests.HandleAsyncForMessage(message);
        }

        private static async Task HandleAsyncForMessage(IStartSurveyMessage message)
        {
            var json = Serializer.SerializeObject(message);
            var data = new MessagePublishedData {Message = new PubsubMessage {TextData = json}};

            var cloudEvent = new CloudEvent
            {
                Type = MessagePublishedData.MessagePublishedCloudEventType,
                Source = new Uri("//pubsub.googleapis.com", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                Data = data
            };

            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new SaveSurveyPubSubClientMock(),
                new GamesDatabaseMock(),
                new PlayerMappingsDatabaseMock());
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}
