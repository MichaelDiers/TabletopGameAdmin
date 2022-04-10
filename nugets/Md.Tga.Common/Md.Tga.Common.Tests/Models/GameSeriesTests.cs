namespace Md.Tga.Common.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Contracts.Model;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="GameSeries" />.
    /// </summary>
    public class GameSeriesTests
    {
        [Fact]
        public void AddToDictionary()
        {
            var obj = GameSeriesTests.Init();
            var dictionary = new Dictionary<string, object>();
            obj.AddToDictionary(dictionary);
            Assert.NotNull(dictionary);
            Assert.Equal(7, dictionary.Count);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
            Assert.Equal(obj.Name, dictionary[NamedBase.NameName]);
        }

        public static GameSeries Create()
        {
            return GameSeriesTests.Init(Guid.NewGuid().ToString());
        }

        [Fact]
        public void ExtendsBase()
        {
            Assert.IsAssignableFrom<Base>(GameSeriesTests.Init());
        }

        [Fact]
        public void ExtendsNamedBase()
        {
            Assert.IsAssignableFrom<NamedBase>(GameSeriesTests.Init());
        }

        [Fact]
        public void FromDictionary()
        {
            var expected = GameSeriesTests.Init();
            var dictionary = expected.ToDictionary();
            var actual = GameSeries.FromDictionary(dictionary);
            Assert.NotNull(expected);
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);

            Assert.Equal(expected.Countries.Count(), actual.Countries.Count());
            foreach (var expectedCountry in expected.Countries)
            {
                Assert.Contains(
                    actual.Countries,
                    country => expectedCountry.Id == country.Id &&
                               expectedCountry.Name == country.Name &&
                               expectedCountry.SideId == country.SideId);
            }

            Assert.Equal(expected.Sides.Count(), actual.Sides.Count());
            foreach (var expectedSide in expected.Sides)
            {
                Assert.Contains(
                    actual.Sides,
                    country => expectedSide.Id == country.Id && expectedSide.Name == country.Name);
            }

            Assert.Equal(expected.Players.Count(), actual.Players.Count());
            foreach (var expectedPlayer in expected.Players)
            {
                Assert.Contains(
                    actual.Players,
                    person => expectedPlayer.Id == person.Id &&
                              expectedPlayer.Name == person.Name &&
                              expectedPlayer.Email == person.Email);
            }

            Assert.Equal(expected.Organizer.Email, actual.Organizer.Email);
            Assert.Equal(expected.Organizer.Name, actual.Organizer.Name);
            Assert.Equal(expected.Organizer.Id, actual.Organizer.Id);
        }


        [Fact]
        public void ImplementsIBase()
        {
            Assert.IsAssignableFrom<IBase>(GameSeriesTests.Init());
        }

        [Fact]
        public void ImplementsIToDictionary()
        {
            Assert.IsAssignableFrom<IToDictionary>(GameSeriesTests.Init());
        }

        [Fact]
        public void Json()
        {
            var expected = GameSeriesTests.Init();
            var actual = JsonConvert.DeserializeObject<GameSeries>(JsonConvert.SerializeObject(expected));
            Assert.NotNull(expected);
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);

            Assert.Equal(expected.Countries.Count(), actual.Countries.Count());
            foreach (var expectedCountry in expected.Countries)
            {
                Assert.Contains(
                    actual.Countries,
                    country => expectedCountry.Id == country.Id &&
                               expectedCountry.Name == country.Name &&
                               expectedCountry.SideId == country.SideId);
            }

            Assert.Equal(expected.Sides.Count(), actual.Sides.Count());
            foreach (var expectedSide in expected.Sides)
            {
                Assert.Contains(
                    actual.Sides,
                    country => expectedSide.Id == country.Id && expectedSide.Name == country.Name);
            }

            Assert.Equal(expected.Players.Count(), actual.Players.Count());
            foreach (var expectedPlayer in expected.Players)
            {
                Assert.Contains(
                    actual.Players,
                    person => expectedPlayer.Id == person.Id &&
                              expectedPlayer.Name == person.Name &&
                              expectedPlayer.Email == person.Email);
            }

            Assert.Equal(expected.Organizer.Email, actual.Organizer.Email);
            Assert.Equal(expected.Organizer.Name, actual.Organizer.Name);
            Assert.Equal(expected.Organizer.Id, actual.Organizer.Id);
        }

        [Fact]
        public void Serialize()
        {
            var obj = GameSeriesTests.Init();
            var actual = JsonConvert.SerializeObject(obj);
            Assert.Equal(GameSeriesTests.SerializePlain(obj), actual);
        }

        public static string SerializePlain(IGameSeries obj)
        {
            var baseJson = NamedBaseTests.SerializePlain(obj);
            var sides = string.Join(",", obj.Sides.Select(NamedBaseTests.SerializePlain));
            var countries = string.Join(",", obj.Countries.Select(CountryTests.SerializePlain));
            var organizer = PersonTests.SerializePlain(obj.Organizer);
            var players = string.Join(",", obj.Players.Select(PersonTests.SerializePlain));
            return
                $"{{{baseJson.Substring(1, baseJson.Length - 2)},\"sides\":[{sides}],\"countries\":[{countries}],\"organizer\":{organizer},\"players\":[{players}],\"gameType\":\"{obj.GameType}\"}}";
        }

        [Fact]
        public void ToDictionary()
        {
            var obj = GameSeriesTests.Init();
            var dictionary = obj.ToDictionary();
            Assert.NotNull(dictionary);
            Assert.Equal(7, dictionary.Count);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
            Assert.Equal(obj.Name, dictionary[NamedBase.NameName]);
        }

        private static GameSeries Init()
        {
            return GameSeriesTests.Init(Guid.NewGuid().ToString());
        }

        private static GameSeries Init(string id)
        {
            var sides = Enumerable.Range(0, 2)
                .Select(i => new NamedBase(Guid.NewGuid().ToString(), $"side_{i}"))
                .ToArray();

            var countries = Enumerable.Range(0, sides.Length * 2)
                .Select(i => new Country(Guid.NewGuid().ToString(), $"country_{i}", sides[i % sides.Length].Id))
                .ToArray();

            var players = Enumerable.Range(0, countries.Length)
                .Select(i => new Person(Guid.NewGuid().ToString(), $"player_{i}", "player@foo.example"))
                .ToArray();
            return new GameSeries(
                id,
                "game series",
                sides,
                countries,
                new Person(Guid.NewGuid().ToString(), "organizer", "organizer@foo.example"),
                players,
                "Game Type");
        }
    }
}
