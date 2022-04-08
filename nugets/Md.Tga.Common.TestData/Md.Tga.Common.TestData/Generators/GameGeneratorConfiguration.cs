namespace Md.Tga.Common.TestData.Generators
{
    using System;

    public class GameGeneratorConfiguration
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string InternalGameSeriesId { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = $"GameName-{Guid.NewGuid().ToString()}";

        public string SurveyId { get; set; } = Guid.NewGuid().ToString();
    }
}
