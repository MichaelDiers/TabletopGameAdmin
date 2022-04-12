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
            return SurveyStatusGenerator.Generate(new SurveyStatusGeneratorConfiguration());
        }

        public static IEnumerable<ISurveyStatus> Generate(SurveyStatusGeneratorConfiguration configuration)
        {
            return SurveyStatusGenerator.Generate(
                new SurveyStatusGeneratorConfiguration(),
                SurveyGenerator.Generate(
                    new SurveyGeneratorConfiguration {DocumentId = configuration.ParentDocumentId}));
        }

        public static IEnumerable<ISurveyStatus> Generate(
            SurveyStatusGeneratorConfiguration configuration,
            ISurvey survey
        )
        {
            if (survey.DocumentId != configuration.ParentDocumentId)
            {
                throw new ArgumentException("id mismatch");
            }

            yield return new SurveyStatus(
                configuration.DocumentId,
                configuration.Created,
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
