namespace Md.Tga.StartSurveySubscriber.Tests
{
    using System;
    using Google.Cloud.Functions.Testing;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.TestData.Generators;
    using Md.Tga.Common.TestData.Mocks.Database;
    using Md.Tga.Common.TestData.Mocks.PubSub;
    using Xunit;

    public class FunctionProviderTests
    {
        [Fact]
        public async void HandleAsync()
        {
            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new SaveSurveyPubSubClientMock(),
                new GamesDatabaseMock(),
                new PlayerMappingsDatabaseMock());
            var container = new TestDataContainer();
            var message = new StartSurveyMessage(Guid.NewGuid().ToString(), container.GameSeries, container.Game);
            await provider.HandleAsync(message);
        }
    }
}
