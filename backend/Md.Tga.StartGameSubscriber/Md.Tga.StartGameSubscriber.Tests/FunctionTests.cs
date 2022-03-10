namespace Md.Tga.StartGameSubscriber.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.StartGameSubscriber.Logic;
    using Md.Tga.StartGameSubscriber.Model;
    using Md.Tga.StartGameSubscriber.Tests.Data;
    using Md.Tga.StartGameSubscriber.Tests.Mocks;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Function" />.
    /// </summary>
    public class FunctionTests
    {
        [Fact]
        public async void HandleAsync()
        {
            var message = TestData.StartGameMessage();
            await HandleAsyncForMessage(message);
        }

        [Fact(Skip = "Integration")]
        public async void HandleAsyncIntegration()
        {
            var message = TestData.StartGameMessage();
            await HandleAsyncForMessageIntegration(message);
        }

        [Fact]
        public async void HandleAsyncWithInternalId()
        {
            var message = TestData.StartGameMessageWithoutGameSeries();
            await HandleAsyncForMessage(message);
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

        private static async Task HandleAsyncForMessageIntegration(IStartGameMessage message)
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
            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new GameSeriesReadOnlyDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.GameSeriesCollectionName)),
                new GamesReadOnlyDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.GamesCollectionName)),
                new TranslationsReadOnlyDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.TranslationsCollectionName),
                    configuration),
                new InitializeSurveyPubSubClient(
                    new PubSubClientConfiguration(configuration.ProjectId, configuration.InitializeSurveyTopicName)),
                new SaveGamePubSubClient(
                    new PubSubClientConfiguration(configuration.ProjectId, configuration.SaveGameTopicName)));
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}
