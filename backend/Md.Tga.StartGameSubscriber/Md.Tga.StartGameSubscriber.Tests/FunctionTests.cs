namespace Md.Tga.StartGameSubscriber.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.Common.Model;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Firestore.Logic;
    using Md.Tga.StartGameSubscriber.Logic;
    using Md.Tga.StartGameSubscriber.Model;
    using Md.Tga.StartGameSubscriber.Tests.Data;
    using Md.Tga.StartGameSubscriber.Tests.Mocks;
    using Newtonsoft.Json;
    using Xunit;
    using Environment = Md.Common.Contracts.Environment;

    /// <summary>
    ///     Tests for <see cref="Function" />.
    /// </summary>
    public class FunctionTests
    {
        [Fact]
        public async void HandleAsync()
        {
            var message = TestData.StartGameMessage();
            await FunctionTests.HandleAsyncForMessage(message);
        }

        //[Theory]
        public async void HandleAsyncIntegration(string projectId)
        {
            var message = TestData.StartGameMessage();
            await FunctionTests.HandleAsyncForMessageIntegration(message, projectId);
        }

        [Fact]
        public async void HandleAsyncWithInternalId()
        {
            var message = TestData.StartGameMessageWithoutGameSeries();
            await FunctionTests.HandleAsyncForMessage(message);
        }

        private static async Task HandleAsyncForMessage(IStartGameMessage message)
        {
            var json = JsonConvert.SerializeObject(message);
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
            var provider = new FunctionProviderMock(message);
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }

        private static async Task HandleAsyncForMessageIntegration(IStartGameMessage message, string projectId)
        {
            var json = JsonConvert.SerializeObject(message);
            var data = new MessagePublishedData {Message = new PubsubMessage {TextData = json}};

            var cloudEvent = new CloudEvent
            {
                Type = MessagePublishedData.MessagePublishedCloudEventType,
                Source = new Uri("//pubsub.googleapis.com", UriKind.RelativeOrAbsolute),
                Id = Guid.NewGuid().ToString(),
                Time = DateTimeOffset.UtcNow,
                Data = data
            };

            var configuration =
                JsonConvert.DeserializeObject<FunctionConfiguration>(
                    await File.ReadAllTextAsync("appsettings.Development.json"));
            Assert.NotNull(configuration);

            var runtime = new RuntimeEnvironment {Environment = Environment.Test, ProjectId = projectId};
            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new GameSeriesReadOnlyDatabase(runtime),
                new TranslationsReadOnlyDatabase(runtime),
                new InitializeSurveyPubSubClient(
                    new PubSubClientConfiguration(projectId, configuration.InitializeSurveyTopicName)),
                new SaveGamePubSubClient(new PubSubClientConfiguration(projectId, configuration.SaveGameTopicName)),
                configuration);

            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}
