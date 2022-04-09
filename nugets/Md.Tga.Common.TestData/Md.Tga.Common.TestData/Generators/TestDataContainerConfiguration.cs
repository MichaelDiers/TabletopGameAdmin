namespace Md.Tga.Common.TestData.Generators
{
    public class TestDataContainerConfiguration
    {
        public GameGeneratorConfiguration GameGeneratorConfiguration { get; set; } = new GameGeneratorConfiguration();

        public GameSeriesGeneratorConfiguration GameSeriesGeneratorConfiguration { get; set; } =
            new GameSeriesGeneratorConfiguration();

        public SurveyGeneratorConfiguration SurveyGeneratorConfiguration { get; set; } =
            new SurveyGeneratorConfiguration();

        public SurveyResultGeneratorConfiguration SurveyResultGeneratorConfiguration { get; set; } =
            new SurveyResultGeneratorConfiguration();

        public SurveyStatusGeneratorConfiguration SurveyStatusGeneratorConfiguration { get; set; } =
            new SurveyStatusGeneratorConfiguration();
    }
}
