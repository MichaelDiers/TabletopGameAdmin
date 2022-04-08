namespace Md.Tga.Common.TestData.Generators
{
    using System;

    public class SurveyGeneratorConfiguration
    {
        public int ChoiceCount { get; set; } = 5;
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Info { get; set; } = $"SurveyInfo-{Guid.NewGuid().ToString()}";
        public string Link { get; set; } = $"SurveyLink-{Guid.NewGuid().ToString()}";

        public string Name { get; set; } = $"SurveyName-{Guid.NewGuid().ToString()}";

        public int ParticipantCount { get; set; } = 5;

        public int QuestionCount { get; set; } = 3;
    }
}
