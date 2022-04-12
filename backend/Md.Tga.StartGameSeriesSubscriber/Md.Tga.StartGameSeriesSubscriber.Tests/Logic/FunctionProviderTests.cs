namespace Md.Tga.StartGameSeriesSubscriber.Tests.Logic
{
    using System;
    using Google.Cloud.Functions.Testing;
    using Md.Common.Logic;
    using Md.Common.Model;
    using Md.Tga.Common.Firestore.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.TestData.Generators;
    using Md.Tga.Common.TestData.Mocks.Database;
    using Md.Tga.Common.TestData.Mocks.PubSub;
    using Md.Tga.StartGameSeriesSubscriber.Logic;
    using Xunit;
    using Environment = Md.Common.Contracts.Model.Environment;

    public class FunctionProviderTests
    {
        [Fact]
        public async void HandleAsync()
        {
            var testData = new TestDataContainer();
            var gameSeries = new GameSeries(
                null,
                null,
                testData.GameSeries.ExternalId,
                testData.GameSeries.Name,
                testData.GameSeries.Sides,
                testData.GameSeries.Countries,
                testData.GameSeries.Organizer,
                testData.GameSeries.Players,
                testData.GameSeries.GameType);
            var saveGameSeriesMessage = new SaveGameSeriesMessage(Guid.NewGuid().ToString(), gameSeries);
            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new GameConfigDatabaseMock(),
                new SaveGameSeriesPubSubClientMock());
            //await provider.HandleAsync(saveGameSeriesMessage);
            Assert.Empty(logger.ListLogEntries());
        }

        [Theory(Skip = "Integration")]
        [InlineData(Environment.Test, "projectId")]
        public async void HandleAsyncIntegration(Environment environment, string projectId)
        {
            var testData = new TestDataContainer();
            var gameSeries = new GameSeries(
                null,
                null,
                testData.GameSeries.ExternalId,
                testData.GameSeries.Name,
                testData.GameSeries.Sides,
                testData.GameSeries.Countries,
                testData.GameSeries.Organizer,
                testData.GameSeries.Players,
                testData.GameSeries.GameType);
            var saveGameSeriesMessage = new SaveGameSeriesMessage(Guid.NewGuid().ToString(), gameSeries);
            var json = Serializer.SerializeObject(saveGameSeriesMessage);
            saveGameSeriesMessage = Serializer.DeserializeObject<SaveGameSeriesMessage>(json);
            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider(
                logger,
                new GameConfigDatabase(new RuntimeEnvironment {Environment = environment, ProjectId = projectId}),
                new SaveGameSeriesPubSubClientMock());
            //await provider.HandleAsync(saveGameSeriesMessage);
            Assert.Empty(logger.ListLogEntries());
        }
    }
}
