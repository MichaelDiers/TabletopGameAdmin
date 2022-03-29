namespace Md.Tga.SurveyClosedSubscriber.Tests.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.SurveyClosedSubscriber.Logic;
    using Md.Tga.SurveyClosedSubscriber.Tests.Data;
    using Surveys.Common.Models;
    using Xunit;

    public class SurveyEvaluatorTests
    {
        [Fact]
        public void DistinctFirstAnswer()
        {
            var surveyId = Guid.NewGuid().ToString();
            var gameSeries = TestData.CreateGameSeries();
            var countries = new List<ICountry>(gameSeries.Countries);
            countries.AddRange(gameSeries.Countries);
            var surveyResults = gameSeries.Players.Select(
                (p, i) => new SurveyResult(
                    surveyId,
                    p.Id,
                    false,
                    countries.Skip(i)
                        .Take(3)
                        .Select(c => new QuestionReference(Guid.NewGuid().ToString(), c.Id))
                        .ToArray()));
            var solution = new SurveyEvaluator().Evaluate(gameSeries, surveyResults).ToArray();
            var players = gameSeries.Players.ToArray();
            for (var i = 0; i < gameSeries.Countries.Count(); ++i)
            {
                Assert.Equal(countries[i].Id, solution[i].CountryId);
                Assert.Equal(players[i].Id, solution[i].PlayerId);
            }
        }

        [Fact]
        public void DuplicatesInAnswers()
        {
            var surveyId = Guid.NewGuid().ToString();
            var gameSeries = TestData.CreateGameSeries();
            var countries = new List<ICountry>(gameSeries.Countries);
            countries.AddRange(gameSeries.Countries);
            var surveyResults = gameSeries.Players.Select(
                    (p, i) => new SurveyResult(
                        surveyId,
                        p.Id,
                        false,
                        countries.Skip(i)
                            .Take(3)
                            .Select(c => new QuestionReference(Guid.NewGuid().ToString(), c.Id))
                            .ToArray()))
                .ToArray();
            surveyResults[0] = new SurveyResult(
                surveyResults[0].InternalSurveyId,
                surveyResults[0].ParticipantId,
                surveyResults[0].IsSuggested,
                new[]
                {
                    new QuestionReference(Guid.NewGuid().ToString(), surveyResults[1].Results.First().ChoiceId),
                    new QuestionReference(Guid.NewGuid().ToString(), surveyResults[0].Results.First().ChoiceId),
                    new QuestionReference(
                        Guid.NewGuid().ToString(),
                        surveyResults[0].Results.Skip(1).First().ChoiceId)
                });

            var solution = new SurveyEvaluator().Evaluate(gameSeries, surveyResults)?.ToArray();
            Assert.NotNull(solution);
            Assert.NotEmpty(solution);
            var players = gameSeries.Players.ToArray();
            for (var i = 0; i < gameSeries.Countries.Count(); ++i)
            {
                Assert.Equal(countries[i].Id, solution[i].CountryId);
                Assert.Equal(players[i].Id, solution[i].PlayerId);
            }
        }
    }
}
