namespace Md.Tga.Common.TestData.Generators
{
    using System.Collections.Generic;
    using Md.Tga.Common.Contracts.Models;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;

    public static class SurveyStatusGenerator
    {
        public static IEnumerable<ISurveyStatus> Generate()
        {
            return SurveyStatusGenerator.Generate(new SurveyStatusGeneratorConfiguration(), GameGenerator.Generate());
        }

        public static IEnumerable<ISurveyStatus> Generate(SurveyStatusGeneratorConfiguration configuration, IGame game)
        {
            yield return new SurveyStatus(game.SurveyDocumentId, Status.Created);
            if (configuration.IsClosed)
            {
                yield return new SurveyStatus(game.SurveyDocumentId, Status.Closed);
            }
        }
    }
}
