namespace Md.Tga.SurveyClosedSubscriber.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using CloudNative.CloudEvents;
    using Google.Cloud.Functions.Testing;
    using Google.Events.Protobuf.Cloud.PubSub.V1;
    using Md.Common.Model;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Firestore.Logic;
    using Md.Tga.Common.Models;
    using Md.Tga.SurveyClosedSubscriber.Logic;
    using Md.Tga.SurveyClosedSubscriber.Tests.Data;
    using Md.Tga.SurveyClosedSubscriber.Tests.Mocks;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Firestore.Models;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;
    using Xunit;
    using Environment = Md.Common.Contracts.Environment;

    /// <summary>
    ///     Tests for <see cref="Function" />.
    /// </summary>
    public class FunctionTests
    {
        //[Fact(Skip = "Integration")]
        [Fact]
        public async void HandleAsync()
        {
            var message = TestData.Initialize();
            await FunctionTests.HandleAsyncForMessage(message);
        }

        [Fact]
        public async void HandleAsyncWithProvider()
        {
            var message = TestData.Initialize();
            await FunctionTests.HandleAsyncForMessageWithProvider(message);
        }

        [Fact(Skip = "Integration")]
        public async void TestDataCreate()
        {
            var projectId = "surveys-services-test";
            var runtime = new RuntimeEnvironment {Environment = Environment.Test, ProjectId = projectId};

            var status = await new SurveyStatusReadOnlyDatabase(runtime).ReadOneAsync(
                SurveyStatus.StatusName,
                Status.Closed.ToString());
            Assert.NotNull(status);

            var survey = await new SurveyReadOnlyDatabase(runtime).ReadByDocumentIdAsync(status.InternalSurveyId);
            Assert.NotNull(survey);

            var game = await new GameReadOnlyDatabase(runtime).ReadOneAsync(Game.SurveyIdName, survey.Id);
            Assert.NotNull(game);

            var gameSeries =
                await new GameSeriesReadOnlyDatabase(runtime).ReadByDocumentIdAsync(game.InternalGameSeriesId);
            Assert.NotNull(gameSeries);

            var results = (await new SurveyResultReadOnlyDatabase(runtime).ReadManyAsync(
                    SurveyResult.InternalSurveyIdName,
                    status.InternalSurveyId,
                    OrderType.Desc)).Where(x => !x.IsSuggested)
                .ToArray();
            Assert.NotNull(results);
            Assert.NotEmpty(results);

            var resultDictionary = new Dictionary<string, ISurveyResult>();
            foreach (var result in results)
            {
                if (!resultDictionary.ContainsKey(result.ParticipantId))
                {
                    resultDictionary.Add(result.ParticipantId, result);
                }
            }

            Assert.Equal(survey.Participants.Count(), resultDictionary.Count);

            var surveyClosedMessage = new SurveyClosedMessage(
                Guid.NewGuid().ToString(),
                survey,
                resultDictionary.Values);

            await File.WriteAllTextAsync(
                "../../../Data/survey-closed-message.json",
                JsonConvert.SerializeObject(surveyClosedMessage));

            await File.WriteAllTextAsync("../../../Data/game.json", JsonConvert.SerializeObject(game));
            await File.WriteAllTextAsync("../../../Data/game-series.json", JsonConvert.SerializeObject(gameSeries));
            await File.WriteAllTextAsync("../../../Data/survey.json", JsonConvert.SerializeObject(survey));
        }

        private static async Task HandleAsyncForMessage(ISurveyClosedMessage message)
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

        private static async Task HandleAsyncForMessageWithProvider(ISurveyClosedMessage message)
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
            var provider = new FunctionProvider(
                logger,
                new GameSeriesDatabaseMock(),
                new GamesDatabaseMock(),
                new SurveyEvaluator(),
                new PubSubClientMock(),
                new PubSubClientMock());
            var function = new Function(logger, provider);
            await function.HandleAsync(cloudEvent, data, CancellationToken.None);

            Assert.Empty(logger.ListLogEntries());
        }
    }
}
