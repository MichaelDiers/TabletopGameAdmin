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
            return GameGenerator.Generate(
                configuration,
                GameSeriesGenerator.Generate(
                    new GameSeriesGeneratorConfiguration {DocumentId = configuration.ParentDocumentId}));
        }

        public static IGame Generate(GameGeneratorConfiguration configuration, IGameSeries gameSeries)
        {
            if (configuration.ParentDocumentId != gameSeries.DocumentId)
            {
                throw new ArgumentException("id mismatch");
            }

            return new Game(
                configuration.DocumentId,
                configuration.Created,
                configuration.ParentDocumentId,
                configuration.Name,
                gameSeries.Players.Select(player => new GameTermination(player.Id, Guid.NewGuid().ToString()))
                    .ToArray());
        }
    }
}
