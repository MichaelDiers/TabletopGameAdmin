namespace Md.Tga.StartGameSeriesSubscriber.Tests.Logic
{
    using Google.Cloud.Functions.Testing;
    using Md.Common.Contracts.Model;
    using Md.Common.Model;
    using Md.Tga.Common.Firestore.Logic;
    using Md.Tga.Common.TestData.Generators;
    using Md.Tga.Common.TestData.Mocks.Database;
    using Md.Tga.Common.TestData.Mocks.PubSub;
    using Md.Tga.StartGameSeriesSubscriber.Logic;
    using Xunit;

    public class FunctionProviderTests
    {
        [Fact]
        public async void HandleAsync()
        {
            var testData = new TestDataContainer();
            var message = testData.StartGameSeriesMessage();
            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new GameConfigDatabaseMock(testData.GameConfig()),
                new SaveGameSeriesPubSubClientMock());
            await provider.HandleAsync(message);
            Assert.Empty(logger.ListLogEntries());
        }

        [Theory(Skip = "Integration")]
        [InlineData(Environment.Test, "projectId")]
        public async void HandleAsyncIntegration(Environment environment, string projectId)
        {
            var testData = new TestDataContainer();
            var message = testData.StartGameSeriesMessage();
            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new GameConfigDatabase(new RuntimeEnvironment {Environment = environment, ProjectId = projectId}),
                new SaveGameSeriesPubSubClientMock());
            await provider.HandleAsync(message);
            Assert.Empty(logger.ListLogEntries());
        }
    }
}
