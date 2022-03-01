namespace TabletopGameAdmin.Common.Tests.Models
{
    using System;
    using System.Linq;
    using TabletopGameAdmin.Common.Contracts.Models;
    using TabletopGameAdmin.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="GameSeries" />.
    /// </summary>
    public class GameSeriesTests
    {
        public static GameSeries Create()
        {
            return Create(Guid.NewGuid().ToString());
        }

        public static GameSeries Create(string id)
        {
            var sides = Enumerable.Range(0, 2).Select(i => new NamedBase(Guid.NewGuid().ToString(), $"side_{i}"))
                .ToArray();

            var countries = Enumerable.Range(0, sides.Length * 2).Select(
                i => new Country(Guid.NewGuid().ToString(), $"country_{i}", sides[i % sides.Length].Id)).ToArray();

            var players = Enumerable.Range(0, countries.Length)
                .Select(i => new Person(Guid.NewGuid().ToString(), $"player_{i}", "player@foo.example")).ToArray();
            return new GameSeries(
                id,
                "game series",
                sides,
                countries,
                new Person(Guid.NewGuid().ToString(), "organizer", "organizer@foo.example"),
                players);
        }

        /// <summary>
        ///     Checks for implemented interfaces.
        /// </summary>
        [Fact]
        public void Implements()
        {
            TestHelper.Implements<GameSeries, IBase, INamedBase, IGameSeries>(Create());
        }
    }
}
