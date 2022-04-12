namespace Md.Tga.Common.TestData.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.TestData.Mocks.Database;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;

    public class TestDataContainer
    {
        public TestDataContainer()
            : this(new TestDataContainerConfiguration())
        {
        }

        public TestDataContainer(TestDataContainerConfiguration configuration)
        {
            this.GameSeries = GameSeriesGenerator.Generate(configuration.GameSeriesGeneratorConfiguration);
            this.Game = GameGenerator.Generate(configuration.GameGeneratorConfiguration, this.GameSeries);
            this.Survey = SurveyGenerator.Generate(
                configuration.SurveyGeneratorConfiguration,
                this.GameSeries,
                this.Game);
            this.SurveyStatus = SurveyStatusGenerator
                .Generate(configuration.SurveyStatusGeneratorConfiguration, this.Survey)
                .ToList();
            this.SurveyResults = SurveyResultGenerator.Generate(
                    configuration.SurveyResultGeneratorConfiguration,
                    this.Game,
                    this.Survey)
                .ToList();

            this.GameSeriesDatabaseMock = new GameSeriesDatabaseMock(this.GameSeries, this.Game);
            this.GamesDatabaseMock = new GamesDatabaseMock(this.Game as Game ?? throw new ArgumentNullException());

            this.SurveysDatabaseMock = new SurveysDatabaseMock(this.Survey, this.SurveyStatus.First());
            this.SurveyStatusDatabaseMock = new SurveyStatusDatabaseMock(this.SurveyStatus);
            this.SurveyResultsDatabaseMock = new SurveyResultsDatabaseMock(this.SurveyResults);
        }

        public IGame Game { get; }

        public GamesDatabaseMock GamesDatabaseMock { get; set; }

        public IGameSeries GameSeries { get; }

        public GameSeriesDatabaseMock GameSeriesDatabaseMock { get; set; }

        public ISurvey Survey { get; }

        public IList<ISurveyResult> SurveyResults { get; }

        public SurveyResultsDatabaseMock SurveyResultsDatabaseMock { get; set; }

        public SurveysDatabaseMock SurveysDatabaseMock { get; set; }

        public IList<ISurveyStatus> SurveyStatus { get; }

        public SurveyStatusDatabaseMock SurveyStatusDatabaseMock { get; set; }

        public IDictionary<string, IGameConfig> GameConfig()
        {
            return new Dictionary<string, IGameConfig>
            {
                {
                    this.StartGameSeriesMessage().GameSeries.GameType,
                    new GameConfig(
                        "game config",
                        this.GameSeries.Countries.Select(
                                c => new GameCountryConfig(
                                    c.Name,
                                    this.GameSeries.Countries.First(cc => cc.Id == c.SideId).Name))
                            .ToArray())
                }
            };
        }

        public IStartGameSeriesMessage StartGameSeriesMessage()
        {
            return new StartGameSeriesMessage(
                Guid.NewGuid().ToString(),
                new StartGameSeries(
                    Guid.NewGuid().ToString(),
                    "name",
                    "AAG40",
                    new StartGameSeriesPerson(this.GameSeries.Organizer.Name, this.GameSeries.Organizer.Email),
                    this.GameSeries.Players.Select(player => new StartGameSeriesPerson(player.Name, player.Email))));
        }

        public ISurveyClosedMessage SurveyClosedMessage()
        {
            return this.SurveyClosedMessage(Guid.NewGuid().ToString());
        }

        public ISurveyClosedMessage SurveyClosedMessage(string processId)
        {
            var results = new Dictionary<string, ISurveyResult>();
            foreach (var surveyResult in this.SurveyResults.Where(sr => !sr.IsSuggested))
            {
                if (!results.TryAdd(surveyResult.ParticipantId, surveyResult))
                {
                    results[surveyResult.ParticipantId] = surveyResult;
                }
            }

            if (results.Count != this.Survey.Participants.Count())
            {
                throw new ArgumentException("results and participants do not match!");
            }

            return new SurveyClosedMessage(processId, this.Survey, results.Values);
        }
    }
}
