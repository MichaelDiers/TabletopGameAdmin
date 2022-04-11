namespace Md.Tga.Common.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Contracts.Database;
    using Md.Common.Contracts.Model;
    using Md.Common.Database;
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
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
            Assert.Equal(9, dictionary.Count);
        }

        public static GameSeries Create()
        {
            return GameSeriesTests.Init(Guid.NewGuid().ToString());
        }

        [Fact]
        public void ExtendsDatabaseObject()
        {
            Assert.IsAssignableFrom<DatabaseObject>(GameSeriesTests.Init());
        }

        [Fact]
        public void FromDictionary()
        {
            var expected = GameSeriesTests.Init();
            var dictionary = expected.ToDictionary();
            var actual = GameSeries.FromDictionary(dictionary);
            GameSeriesTests.CheckEqual(expected, actual);
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
            var actual = Serializer.DeserializeObject<GameSeries>(Serializer.SerializeObject(expected));

            GameSeriesTests.CheckEqual(expected, actual);
        }

        [Fact]
        public void Serialize()
        {
            var obj = GameSeriesTests.Init();
            var actual = Serializer.SerializeObject(obj);
            Assert.Equal(GameSeriesTests.SerializePlain(obj), actual);
        }

        public static string SerializePlain(IGameSeries obj)
        {
            var basePartial = GameSeriesTests.SerializePlainPartial(obj);
            var sides = string.Join(",", obj.Sides.Select(NamedBaseTests.SerializePlain));
            var countries = string.Join(",", obj.Countries.Select(CountryTests.SerializePlain));
            var organizer = PersonTests.SerializePlain(obj.Organizer);
            var players = string.Join(",", obj.Players.Select(PersonTests.SerializePlain));
            return
                $"{{{basePartial},\"name\":\"{obj.Name}\",\"sides\":[{sides}],\"countries\":[{countries}],\"organizer\":{organizer},\"players\":[{players}],\"gameType\":\"{obj.GameType}\"}}";
        }

        public static string SerializePlainPartial(IDatabaseObject databaseObject)
        {
            var parent = databaseObject.ParentDocumentId == null ? "null" : $"\"{databaseObject.ParentDocumentId}\"";
            Assert.NotNull(databaseObject.Created);
            return
                $"\"documentId\":\"{databaseObject.DocumentId}\",\"created\":{Serializer.SerializeObject(databaseObject.Created)},\"parentDocumentId\":{parent}";
        }

        [Fact]
        public void ToDictionary()
        {
            var obj = GameSeriesTests.Init();
            var dictionary = obj.ToDictionary();
            Assert.NotNull(dictionary);
            Assert.Equal(9, dictionary.Count);
            GameSeriesTests.CheckEqual(obj, GameSeries.FromDictionary(dictionary));
        }

        private static void CheckEqual(IGameSeries expected, IGameSeries actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);

            Assert.Equal(expected.DocumentId, actual.DocumentId);
            Assert.Equal(expected.Created, actual.Created);
            Assert.Equal(expected.ParentDocumentId, actual.ParentDocumentId);

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.GameType, actual.GameType);

            Assert.Equal(expected.Organizer.Email, actual.Organizer.Email);
            Assert.Equal(expected.Organizer.Name, actual.Organizer.Name);
            Assert.Equal(expected.Organizer.Id, actual.Organizer.Id);

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
        }

        private static GameSeries Init()
        {
            return GameSeriesTests.Init(Guid.NewGuid().ToString());
        }

        private static GameSeries Init(string id)
        {
            var sides = Enumerable.Range(0, 2).Select(i => new Side(Guid.NewGuid().ToString(), $"side_{i}")).ToArray();

            var countries = Enumerable.Range(0, sides.Length * 2)
                .Select(i => new Country(Guid.NewGuid().ToString(), $"country_{i}", sides[i % sides.Length].Id))
                .ToArray();

            var players = Enumerable.Range(0, countries.Length)
                .Select(i => new Person(Guid.NewGuid().ToString(), $"player_{i}", "player@foo.example"))
                .ToArray();
            return new GameSeries(
                id,
                DateTime.Now,
                "game series",
                sides,
                countries,
                new Person(Guid.NewGuid().ToString(), "organizer", "organizer@foo.example"),
                players,
                "Game Type");
        }
    }
}
