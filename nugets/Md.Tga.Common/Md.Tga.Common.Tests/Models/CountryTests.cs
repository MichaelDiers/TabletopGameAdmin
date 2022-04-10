namespace Md.Tga.Common.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Md.Common.Contracts.Model;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Country" />.
    /// </summary>
    public class CountryTests
    {
        [Fact]
        public void AddToDictionary()
        {
            var obj = CountryTests.Init();
            var dictionary = new Dictionary<string, object>();
            obj.AddToDictionary(dictionary);
            Assert.NotNull(dictionary);
            Assert.Equal(3, dictionary.Count);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
            Assert.Equal(obj.Name, dictionary[NamedBase.NameName]);
            Assert.Equal(obj.SideId, dictionary[Country.SideIdName]);
        }

        [Fact]
        public void Ctor()
        {
            var id = Guid.NewGuid().ToString();
            const string name = "name";
            var sideId = Guid.NewGuid().ToString();
            var obj = new Country(id, name, sideId);
            Assert.Equal(id, obj.Id);
            Assert.Equal(name, obj.Name);
            Assert.Equal(sideId, obj.SideId);
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void CtorThrowsArgumentExceptionForInvalidId(string id)
        {
            Assert.Throws<ArgumentException>(() => new Country(id, "name", Guid.NewGuid().ToString()));
        }

        [Theory]
        [InlineData("")]
        public void CtorThrowsArgumentExceptionForInvalidName(string name)
        {
            Assert.Throws<ArgumentException>(
                () => new Country(Guid.NewGuid().ToString(), name, Guid.NewGuid().ToString()));
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void CtorThrowsArgumentExceptionForInvalidSideId(string sideId)
        {
            Assert.Throws<ArgumentException>(() => new Country(Guid.NewGuid().ToString(), "name", sideId));
        }

        [Fact]
        public void ExtendsBase()
        {
            Assert.IsAssignableFrom<Base>(CountryTests.Init());
        }

        [Fact]
        public void ExtendsNamedBase()
        {
            Assert.IsAssignableFrom<NamedBase>(CountryTests.Init());
        }

        [Fact]
        public void FromDictionary()
        {
            var expected = CountryTests.Init();
            var dictionary = expected.ToDictionary();
            var actual = Country.FromDictionary(dictionary);
            Assert.NotNull(expected);
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.SideId, actual.SideId);
        }


        [Fact]
        public void ImplementsIBase()
        {
            Assert.IsAssignableFrom<IBase>(CountryTests.Init());
        }

        [Fact]
        public void ImplementsIToDictionary()
        {
            Assert.IsAssignableFrom<IToDictionary>(CountryTests.Init());
        }

        [Fact]
        public void Json()
        {
            var expected = CountryTests.Init();
            var actual = JsonConvert.DeserializeObject<Country>(JsonConvert.SerializeObject(expected));
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.SideId, actual.SideId);
        }

        [Fact]
        public void Serialize()
        {
            var obj = new Country(Guid.NewGuid().ToString(), "the name", Guid.NewGuid().ToString());
            var actual = JsonConvert.SerializeObject(obj);
            Assert.Equal(CountryTests.SerializePlain(obj), actual);
        }

        public static string SerializePlain(ICountry obj)
        {
            var baseJson = NamedBaseTests.SerializePlain(obj);
            return $"{{{baseJson.Substring(1, baseJson.Length - 2)},\"sideId\":\"{obj.SideId}\"}}";
        }

        [Fact]
        public void ToDictionary()
        {
            var obj = CountryTests.Init();
            var dictionary = obj.ToDictionary();
            Assert.NotNull(dictionary);
            Assert.Equal(3, dictionary.Count);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
            Assert.Equal(obj.Name, dictionary[NamedBase.NameName]);
            Assert.Equal(obj.SideId, dictionary[Country.SideIdName]);
        }

        private static Country Init()
        {
            return new Country(Guid.NewGuid().ToString(), "name", Guid.NewGuid().ToString());
        }
    }
}
