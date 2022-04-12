namespace Md.Tga.Common.TestData.Generators
{
    public class TestDataContainerConfiguration
    {
        public TestDataContainerConfiguration()

        {
            this.GameSeriesGeneratorConfiguration = new GameSeriesGeneratorConfiguration();
            this.GameGeneratorConfiguration = new GameGeneratorConfiguration
            {
                ParentDocumentId = this.GameSeriesGeneratorConfiguration.DocumentId
            };
            this.SurveyGeneratorConfiguration =
                new SurveyGeneratorConfiguration {ParentDocumentId = this.GameGeneratorConfiguration.DocumentId};
            this.SurveyResultGeneratorConfiguration =
                new SurveyResultGeneratorConfiguration
                {
                    ParentDocumentId = this.SurveyGeneratorConfiguration.DocumentId
                };
            this.SurveyStatusGeneratorConfiguration =
                new SurveyStatusGeneratorConfiguration
                {
                    ParentDocumentId = this.SurveyGeneratorConfiguration.DocumentId
                };
        }

        public GameGeneratorConfiguration GameGeneratorConfiguration { get; set; }

        public GameSeriesGeneratorConfiguration GameSeriesGeneratorConfiguration { get; set; }

        public SurveyGeneratorConfiguration SurveyGeneratorConfiguration { get; set; }

        public SurveyResultGeneratorConfiguration SurveyResultGeneratorConfiguration { get; set; }

        public SurveyStatusGeneratorConfiguration SurveyStatusGeneratorConfiguration { get; set; }
    }
}
