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
    ///     Tests for <see cref="Person" />.
    /// </summary>
    public class PersonTests
    {
        [Fact]
        public void AddToDictionary()
        {
            var obj = Init();
            var dictionary = new Dictionary<string, object>();
            obj.AddToDictionary(dictionary);
            Assert.NotNull(dictionary);
            Assert.Equal(3, dictionary.Count);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
            Assert.Equal(obj.Name, dictionary[NamedBase.NameName]);
            Assert.Equal(obj.Email, dictionary[Person.EmailName]);
        }

        [Fact]
        public void Ctor()
        {
            var id = Guid.NewGuid().ToString();
            const string name = "name";
            const string email = "foo@bar.example";
            var obj = new Person(id, name, email);
            Assert.Equal(id, obj.Id);
            Assert.Equal(name, obj.Name);
            Assert.Equal(email, obj.Email);
        }

        [Theory]
        [InlineData("")]
        public void CtorThrowsArgumentExceptionForInvalidEmail(string email)
        {
            Assert.Throws<ArgumentException>(() => new Country(Guid.NewGuid().ToString(), "name", email));
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void CtorThrowsArgumentExceptionForInvalidId(string id)
        {
            Assert.Throws<ArgumentException>(() => new Person(id, "name", "foo@bar.example"));
        }

        [Theory]
        [InlineData("")]
        public void CtorThrowsArgumentExceptionForInvalidName(string name)
        {
            Assert.Throws<ArgumentException>(() => new Person(Guid.NewGuid().ToString(), name, "foo@bar.example"));
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
            var actual = Person.FromDictionary(dictionary);
            Assert.NotNull(expected);
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Email, actual.Email);
        }


        [Fact]
        public void ImplementsIBase()
        {
            Assert.IsAssignableFrom<IBase>(Init());
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
            var actual = JsonConvert.DeserializeObject<Person>(JsonConvert.SerializeObject(expected));
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Email, actual.Email);
        }

        [Fact]
        public void Serialize()
        {
            var obj = new Person(Guid.NewGuid().ToString(), "the name", "email@foo.example");
            var actual = JsonConvert.SerializeObject(obj);
            Assert.Equal(SerializePlain(obj), actual);
        }

        public static string SerializePlain(IPerson obj)
        {
            var baseJson = NamedBaseTests.SerializePlain(obj);
            return $"{{{baseJson.Substring(1, baseJson.Length - 2)},\"email\":\"{obj.Email}\"}}";
        }

        [Fact]
        public void ToDictionary()
        {
            var obj = Init();
            var dictionary = obj.ToDictionary();
            Assert.NotNull(dictionary);
            Assert.Equal(3, dictionary.Count);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
            Assert.Equal(obj.Name, dictionary[NamedBase.NameName]);
            Assert.Equal(obj.Email, dictionary[Person.EmailName]);
        }

        private static Person Init()
        {
            return new Person(Guid.NewGuid().ToString(), "name", "foo@bar.example");
        }
    }
}
