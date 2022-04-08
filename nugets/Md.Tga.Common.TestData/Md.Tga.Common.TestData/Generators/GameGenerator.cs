namespace Md.Tga.Common.TestData.Generators
{
    using System;
    using System.Linq;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;

    public static class GameGenerator
    {
        public static IGame Generate()
        {
            return GameGenerator.Generate(new GameGeneratorConfiguration());
        }

        public static IGame Generate(GameGeneratorConfiguration configuration)
        {
            return GameGenerator.Generate(configuration, GameSeriesGenerator.Generate());
        }

        public static IGame Generate(GameGeneratorConfiguration configuration, IGameSeries gameSeries)
        {
            if (gameSeries.Id == configuration.InternalGameSeriesId)
            {
                throw new ArgumentException(
                    "Identical GameSeries.Id and GameGeneratorConfiguration.InternalGameSeriesId");
            }

            return new Game(
                configuration.Id,
                configuration.Name,
                configuration.InternalGameSeriesId,
                configuration.SurveyId,
                gameSeries.Players.Select(player => new GameTermination(player.Id, Guid.NewGuid().ToString()))
                    .ToArray());
        }
    }
}
