namespace Md.Tga.Common.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Md.GoogleCloud.Base.Contracts.Logic;
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
            var obj = Init();
            var dictionary = new Dictionary<string, object>();
            obj.AddToDictionary(dictionary);
            Assert.NotNull(dictionary);
            Assert.Equal(4, dictionary.Count);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
            Assert.Equal(obj.Name, dictionary[NamedBase.NameName]);
            Assert.Equal(obj.InternalGameSeriesId, dictionary[Game.InternalGameSeriesIdName]);
            Assert.Equal(obj.SurveyId, dictionary[Game.SurveyIdName]);
        }

        [Fact]
        public void Ctor()
        {
            var id = Guid.NewGuid().ToString();
            var name = "name";
            var internalGameSeriesId = Guid.NewGuid().ToString();
            var surveyId = Guid.NewGuid().ToString();

            var obj = new Game(id, name, internalGameSeriesId, surveyId);

            Assert.Equal(id, obj.Id);
            Assert.Equal(name, obj.Name);
            Assert.Equal(internalGameSeriesId, obj.InternalGameSeriesId);
            Assert.Equal(surveyId, obj.SurveyId);
        }


        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void CtorThrowsArgumentExceptionForInvalidId(string id)
        {
            Assert.Throws<ArgumentException>(() =>
                new Game(id, "name of the game", Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void CtorThrowsArgumentExceptionForInvalidInternalGameSeriesId(string id)
        {
            Assert.Throws<ArgumentException>(() =>
                new Game(Guid.NewGuid().ToString(), "name of the game", id, Guid.NewGuid().ToString()));
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void CtorThrowsArgumentExceptionForInvalidSurveyId(string id)
        {
            Assert.Throws<ArgumentException>(() =>
                new Game(Guid.NewGuid().ToString(), "name of the game", Guid.NewGuid().ToString(), id));
        }

        [Theory]
        [InlineData("")]
        public void CtorThrowsArgumentExceptionForInvalidName(string name)
        {
            Assert.Throws<ArgumentException>(() =>
                new Game(Guid.NewGuid().ToString(), name, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));
        }

        [Fact]
        public void ExtendsBase()
        {
            Assert.IsAssignableFrom<Base>(Init());
        }

        [Fact]
        public void ExtendsNamedBase()
        {
            Assert.IsAssignableFrom<NamedBase>(Init());
        }

        [Fact]
        public void FromDictionary()
        {
            var expected = Init();
            var dictionary = expected.ToDictionary();
            var actual = Game.FromDictionary(dictionary);
            Assert.NotNull(expected);
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.InternalGameSeriesId, actual.InternalGameSeriesId);
            Assert.Equal(expected.SurveyId, actual.SurveyId);
        }


        [Fact]
        public void ImplementsIBase()
        {
            Assert.IsAssignableFrom<IBase>(Init());
        }

        [Fact]
        public void ImplementsIGame()
        {
            Assert.IsAssignableFrom<IGame>(Init());
        }

        [Fact]
        public void ImplementsIToDictionary()
        {
            Assert.IsAssignableFrom<IToDictionary>(Init());
        }

        [Fact]
        public void Json()
        {
            var expected = Init();
            var actual = JsonConvert.DeserializeObject<Game>(JsonConvert.SerializeObject(expected));
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.InternalGameSeriesId, actual.InternalGameSeriesId);
            Assert.Equal(expected.SurveyId, actual.SurveyId);
        }

        [Fact]
        public void ToDictionary()
        {
            var obj = Init();
            var dictionary = obj.ToDictionary();
            Assert.NotNull(dictionary);
            Assert.Equal(4, dictionary.Count);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
            Assert.Equal(obj.Name, dictionary[NamedBase.NameName]);
            Assert.Equal(obj.InternalGameSeriesId, dictionary[Game.InternalGameSeriesIdName]);
            Assert.Equal(obj.SurveyId, dictionary[Game.SurveyIdName]);
        }

        public static Game Init()
        {
            return new Game(Guid.NewGuid().ToString(), "name of the game", Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());
        }

        [Fact]
        public void Serialize()
        {
            var obj = new Game(Guid.NewGuid().ToString(), "game name", Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());
            var actual = JsonConvert.SerializeObject(obj);
            Assert.Equal(SerializePlain(obj), actual);
        }

        public static string SerializePlain(IGame obj)
        {
            var baseJson = NamedBaseTests.SerializePlain(obj);
            return
                $"{{{baseJson.Substring(1, baseJson.Length - 2)},\"internalGameSeriesId\":\"{obj.InternalGameSeriesId}\",\"surveyId\":\"{obj.SurveyId}\"}}";
        }
    }
}
