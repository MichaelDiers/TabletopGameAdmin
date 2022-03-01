﻿namespace Md.Tga.InitializeGameSeriesSubscriber.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.TabletopGameAdmin.Common.Contracts.Messages;
    using Md.Tga.InitializeGameSeriesSubscriber.Tests.Data;
    using Md.Tga.InitializeGameSeriesSubscriber.Tests.Mocks;
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
            var message = TestData.InitializeGameSeriesMessage();
            await HandleAsyncForMessage(message);
        }

        private static async Task HandleAsyncForMessage(IInitializeGameSeriesMessage message)
        {
            var json = JsonConvert.SerializeObject(message);
            var data = new MessagePublishedData
            {
                Message = new PubsubMessage
                {
                    TextData = json
                }
            };

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
    }
}
