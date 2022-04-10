namespace Md.Tga.Common.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Contracts.Model;
    using Md.Common.Database;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Game" />.
    /// </summary>
    public class GameTests
    {
        [Fact]
        public void AddToDictionary()
        {
            var obj = GameTests.Init();
            var dictionary = new Dictionary<string, object>();
            obj.AddToDictionary(dictionary);
            Assert.NotNull(dictionary);
            Assert.Equal(6, dictionary.Count);
        }

        [Fact]
        public void Ctor()
        {
            var documentId = Guid.NewGuid().ToString();
            var created = DateTime.Now;
            var parentDocumentId = Guid.NewGuid().ToString();

            const string name = "name";
            var surveyDocumentId = Guid.NewGuid().ToString();
            var terminations = new[] {new GameTermination(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())};

            var obj = new Game(
                documentId,
                created,
                parentDocumentId,
                name,
                surveyDocumentId,
                terminations);

            Assert.Equal(documentId, obj.DocumentId);
            Assert.Equal(created, obj.Created);
            Assert.Equal(parentDocumentId, obj.ParentDocumentId);
            Assert.Equal(name, obj.Name);
            Assert.Equal(surveyDocumentId, obj.SurveyDocumentId);
            Assert.Single(obj.GameTerminations);
            Assert.Equal(terminations.First().PlayerId, obj.GameTerminations.First().PlayerId);
            Assert.Equal(terminations.First().TerminationId, obj.GameTerminations.First().TerminationId);
        }

        [Fact]
        public void ExtendsNamedBase()
        {
            Assert.IsAssignableFrom<DatabaseObject>(GameTests.Init());
        }

        [Fact]
        public void FromDictionary()
        {
            var expected = GameTests.Init();
            var dictionary = expected.ToDictionary();
            var actual = Game.FromDictionary(dictionary);
            GameTests.CheckEqual(expected, actual);
        }

        [Fact]
        public void ImplementsIGame()
        {
            Assert.IsAssignableFrom<IGame>(GameTests.Init());
        }

        [Fact]
        public void ImplementsIToDictionary()
        {
            Assert.IsAssignableFrom<IToDictionary>(GameTests.Init());
        }

        public static Game Init()
        {
            return new Game(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                "name of the game",
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new[] {new GameTermination(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())});
        }

        [Fact]
        public void Json()
        {
            var expected = GameTests.Init();
            var actual = JsonConvert.DeserializeObject<Game>(JsonConvert.SerializeObject(expected));
            Assert.NotNull(actual);
            GameTests.CheckEqual(expected, actual);
        }

        [Fact]
        public void Serialize()
        {
            var obj = new Game(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                "game name",
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                new[] {new GameTermination(Guid.NewGuid().ToString(), Guid.NewGuid().ToString())});

            var actual = JsonConvert.SerializeObject(obj);
            Assert.Equal(GameTests.SerializePlain(obj), actual);
        }

        public static string SerializePlain(IGame obj)
        {
            var baseJson = GameSeriesTests.SerializePlainPartial(obj);
            var gameTerminations = string.Join(
                ",",
                obj.GameTerminations.Select(
                    gt => $"{{\"playerId\":\"{gt.PlayerId}\",\"terminationId\":\"{gt.TerminationId}\"}}"));
            return
                $"{{{baseJson},\"name\":\"{obj.Name}\",\"surveyDocumentId\":\"{obj.SurveyDocumentId}\",\"gameTerminations\":[{gameTerminations}]}}";
        }

        [Fact]
        public void ToDictionary()
        {
            var obj = GameTests.Init();
            var dictionary = obj.ToDictionary();
            Assert.NotNull(dictionary);
            Assert.Equal(6, dictionary.Count);
            GameTests.CheckEqual(obj, Game.FromDictionary(dictionary));
        }

        private static void CheckEqual(IGame expected, IGame actual)
        {
            Assert.NotNull(expected);
            Assert.NotNull(actual);
            Assert.Equal(expected.DocumentId, actual.DocumentId);
            Assert.Equal(expected.Created, actual.Created);
            Assert.Equal(expected.ParentDocumentId, actual.ParentDocumentId);

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.SurveyDocumentId, actual.SurveyDocumentId);
            Assert.Equal(expected.GameTerminations.Count(), actual.GameTerminations.Count());
            foreach (var expectedGameTermination in expected.GameTerminations)
            {
                Assert.Contains(
                    actual.GameTerminations,
                    actualGameTermination => expectedGameTermination.PlayerId == actualGameTermination.PlayerId &&
                                             actualGameTermination.TerminationId ==
                                             expectedGameTermination.TerminationId);
            }
        }
    }
}
