namespace Md.Tga.Common.TestData.Generators
{
    public class SurveyResultGeneratorConfiguration : BaseGeneratorConfiguration
    {
        public SurveyResultGeneratorConfigurationStatus Status { get; set; } =
            SurveyResultGeneratorConfigurationStatus.AllVotedDifferentFirstAnswer;
    }
}
