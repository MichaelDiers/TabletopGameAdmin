namespace Md.Tga.Common.TestData.Generators
{
    using System.Collections.Generic;
    using System.Linq;
    using Md.Tga.Common.Contracts.Models;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;

    public class SurveyResultGenerator
    {
        public IEnumerable<ISurveyResult> Generate(
            SurveyResultGeneratorConfiguration configuration,
            IGame game,
            ISurvey survey
        )
        {
            foreach (var surveyParticipant in survey.Participants)
            {
                yield return new SurveyResult(
                    game.SurveyId,
                    surveyParticipant.Id,
                    true,
                    surveyParticipant.QuestionReferences);
            }

            if (configuration.Status == SurveyResultGeneratorConfigurationStatus.AllVotedIdentical)
            {
                var questionReferences = survey.Questions
                    .Select(q => new QuestionReference(q.Id, q.Choices.First(c => c.Selectable).Id))
                    .ToArray();
                foreach (var surveyParticipant in survey.Participants)
                {
                    yield return new SurveyResult(
                        game.SurveyId,
                        surveyParticipant.Id,
                        true,
                        questionReferences);
                }
            }
            else if (configuration.Status == SurveyResultGeneratorConfigurationStatus.AllVotedDifferentFirstAnswer)
            {
                var i = 0;
                foreach (var surveyParticipant in survey.Participants)
                {
                    var questionReferences = survey.Questions.Select(
                            q => new QuestionReference(q.Id, q.Choices.Where(c => c.Selectable).Skip(i).First().Id))
                        .ToArray();
                    yield return new SurveyResult(
                        game.SurveyId,
                        surveyParticipant.Id,
                        true,
                        questionReferences);
                    i += 1;
                }
            }
            else if (configuration.Status == SurveyResultGeneratorConfigurationStatus.IncompleteVotes)
            {
                var questionReferences = survey.Questions
                    .Select(q => new QuestionReference(q.Id, q.Choices.First(c => c.Selectable).Id))
                    .ToArray();
                foreach (var surveyParticipant in survey.Participants.Skip(1))
                {
                    yield return new SurveyResult(
                        game.SurveyId,
                        surveyParticipant.Id,
                        true,
                        questionReferences);
                }
            }
            else if (configuration.Status == SurveyResultGeneratorConfigurationStatus.NoVotes)
            {
                // no additional results
            }
        }
    }
}
