namespace Md.Tga.Common.TestData.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.TestData.Mocks.Database;
    using Surveys.Common.Contracts;

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
                .Generate(configuration.SurveyStatusGeneratorConfiguration, this.Game)
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
    }
}
