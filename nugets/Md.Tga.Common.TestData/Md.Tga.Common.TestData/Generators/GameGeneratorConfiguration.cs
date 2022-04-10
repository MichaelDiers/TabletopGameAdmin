namespace Md.Tga.Common.TestData.Generators
{
    using System;

    public class GameGeneratorConfiguration
    {
        public string DocumentId { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; } = $"GameName-{Guid.NewGuid().ToString()}";

        public string ParentDocumentId { get; set; } = Guid.NewGuid().ToString();

        public string SurveyDocumentId { get; set; } = Guid.NewGuid().ToString();
    }
}
