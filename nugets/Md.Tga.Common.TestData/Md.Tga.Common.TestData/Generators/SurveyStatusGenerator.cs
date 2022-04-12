namespace Md.Tga.Common.TestData.Generators
{
    using System;
    using System.Collections.Generic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;

    public static class SurveyStatusGenerator
    {
        public static IEnumerable<ISurveyStatus> Generate()
        {
            return SurveyStatusGenerator.Generate(new SurveyStatusGeneratorConfiguration(), SurveyGenerator.Generate());
        }

        public static IEnumerable<ISurveyStatus> Generate(
            SurveyStatusGeneratorConfiguration configuration,
            ISurvey survey
        )
        {
            yield return new SurveyStatus(
                configuration.DocumentId,
                DateTime.Now,
                survey.DocumentId,
                Status.Created);
            if (configuration.IsClosed)
            {
                yield return new SurveyStatus(
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
                    survey.DocumentId,
                    Status.Closed);
            }
        }
    }
}
