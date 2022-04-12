namespace Md.Tga.Common.TestData.Generators
{
    using System;
    using System.Linq;
    using Md.Tga.Common.Contracts.Models;
    using Surveys.Common.Contracts;
    using Surveys.Common.Models;

    public static class SurveyGenerator
    {
        public static ISurvey Generate()
        {
            return SurveyGenerator.Generate(new SurveyGeneratorConfiguration());
        }

        public static ISurvey Generate(SurveyGeneratorConfiguration configuration)
        {
            return SurveyGenerator.Generate(
                configuration,
                GameSeriesGenerator.Generate(
                    new GameSeriesGeneratorConfiguration
                    {
                        CountryCount = configuration.ChoiceCount, PlayerCount = configuration.ParticipantCount
                    }));
        }

        public static ISurvey Generate(SurveyGeneratorConfiguration configuration, IGameSeries gameSeries)
        {
            return SurveyGenerator.Generate(
                configuration,
                gameSeries,
                GameGenerator.Generate(
                    new GameGeneratorConfiguration {SurveyDocumentId = configuration.DocumentId},
                    gameSeries));
        }

        public static ISurvey Generate(SurveyGeneratorConfiguration configuration, IGameSeries gameSeries, IGame game)
        {
            if (configuration.ParentDocumentId != game.DocumentId)
            {
                throw new ArgumentException("document id mismatch");
            }

            if (gameSeries.Players.Count() != configuration.ParticipantCount)
            {
                throw new ArgumentException(
                    "Count mismatch: SurveyGeneratorConfiguration.ParticipantCount and GameSeries.Players.Count()");
            }

            if (gameSeries.Countries.Count() != configuration.ChoiceCount)
            {
                throw new ArgumentException(
                    "Count mismatch: SurveyGeneratorConfiguration.ChoicesCount and GameSeries.Countries.Count()");
            }

            var choices = gameSeries.Countries.Select(
                    (country, i) => new Choice(
                        country.Id,
                        country.Name,
                        true,
                        i + 1))
                .Append(
                    new Choice(
                        Guid.NewGuid().ToString(),
                        "ChoiceAnswer",
                        false,
                        0))
                .ToArray();

            var questions = Enumerable.Range(0, configuration.QuestionCount)
                .Select(
                    i => new Question(
                        Guid.NewGuid().ToString(),
                        $"QuestionText-{i}",
                        choices,
                        i))
                .ToArray();
            var questionReferences = questions.Select(q => new QuestionReference(q.Id, choices[0].Id)).ToArray();

            var participants = gameSeries.Players.Select(
                    (p, i) => new Participant(
                        p.Id,
                        p.Email,
                        p.Name,
                        questionReferences,
                        i))
                .ToArray();

            return new Survey(
                configuration.DocumentId,
                configuration.Created,
                game.DocumentId,
                configuration.Name,
                configuration.Info,
                configuration.Link,
                new Person(gameSeries.Organizer.Id, gameSeries.Organizer.Email, gameSeries.Organizer.Name),
                participants,
                questions);
        }
    }
}
