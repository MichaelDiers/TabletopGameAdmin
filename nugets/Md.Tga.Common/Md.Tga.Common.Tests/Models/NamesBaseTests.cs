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
    ///     Tests for <see cref="NamedBase" />.
    /// </summary>
    public class NamesBaseTests
    {
        [Fact]
        public void AddToDictionary()
        {
            var obj = Init();
            var dictionary = new Dictionary<string, object>();
            obj.AddToDictionary(dictionary);
            Assert.NotNull(dictionary);
            Assert.Equal(2, dictionary.Count);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
            Assert.Equal(obj.Name, dictionary[NamedBase.NameName]);
        }

        [Fact]
        public void Ctor()
        {
            var id = Guid.NewGuid().ToString();
            var name = "name";
            var obj = new NamedBase(id, name);
            Assert.Equal(id, obj.Id);
            Assert.Equal(name, obj.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void CtorThrowsArgumentExceptionForInvalidId(string id)
        {
            Assert.Throws<ArgumentException>(() => new NamedBase(id, "name"));
        }

        [Theory]
        [InlineData("")]
        public void CtorThrowsArgumentExceptionForInvalidName(string name)
        {
            Assert.Throws<ArgumentException>(() => new NamedBase(Guid.NewGuid().ToString(), name));
        }

        [Fact]
        public void ExtendsBase()
        {
            Assert.IsAssignableFrom<Base>(Init());
        }

        [Fact]
        public void FromDictionary()
        {
            var expected = Init();
            var dictionary = expected.ToDictionary();
            var actual = NamedBase.FromDictionary(dictionary);
            Assert.NotNull(expected);
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
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
            var actual = JsonConvert.DeserializeObject<NamedBase>(JsonConvert.SerializeObject(expected));
            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
        }

        [Fact]
        public void ToDictionary()
        {
            var obj = Init();
            var dictionary = obj.ToDictionary();
            Assert.NotNull(dictionary);
            Assert.Equal(2, dictionary.Count);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
            Assert.Equal(obj.Name, dictionary[NamedBase.NameName]);
        }

        private static NamedBase Init()
        {
            return new NamedBase(Guid.NewGuid().ToString(), "name");
        }
    }
}
