namespace Md.Tga.Common.TestData.Generators
{
    using System;
    using System.Linq;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;

    public static class GameSeriesGenerator
    {
        public static IGameSeries Generate()
        {
            return GameSeriesGenerator.Generate(new GameSeriesGeneratorConfiguration());
        }

        public static IGameSeries Generate(GameSeriesGeneratorConfiguration configuration)
        {
            var sides = Enumerable.Range(0, configuration.SideCount)
                .Select(i => new Side(Guid.NewGuid().ToString(), $"Side-{i}"))
                .ToArray();
            var countries = Enumerable.Range(0, configuration.CountryCount)
                .Select(
                    i => new Country(Guid.NewGuid().ToString(), $"Country-{i}", sides[i % configuration.SideCount].Id))
                .ToArray();
            var players = Enumerable.Range(0, configuration.PlayerCount)
                .Select(i => new Person(Guid.NewGuid().ToString(), $"Player-{i}", $"player-{i}@example.example"))
                .ToArray();
            return new GameSeries(
                configuration.DocumentId,
                DateTime.Now,
                configuration.Name,
                sides,
                countries,
                new Person(
                    Guid.NewGuid().ToString(),
                    $"OrganizerName-{Guid.NewGuid().ToString()}",
                    "organizer@example.example"),
                players,
                configuration.GameType);
        }
    }
}
