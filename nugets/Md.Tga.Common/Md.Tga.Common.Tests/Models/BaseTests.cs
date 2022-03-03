namespace Md.Tga.Common.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Xunit;

    public class BaseTests
    {
        [Fact]
        public void AddToDictionary()
        {
            var obj = Init();
            var dictionary = new Dictionary<string, object>();
            obj.AddToDictionary(dictionary);
            Assert.NotNull(dictionary);
            Assert.Single(dictionary);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
        }

        [Fact]
        public void Ctor()
        {
            var id = Guid.NewGuid().ToString();
            Assert.Equal(id, new BaseImplementation(id).Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void CtorThrowsArgumentException(string id)
        {
            Assert.Throws<ArgumentException>(() => new BaseImplementation(id));
        }

        [Fact]
        public void ExtendsBase()
        {
            Assert.IsAssignableFrom<Base>(Init());
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
        public void ToDictionary()
        {
            var obj = Init();
            var dictionary = obj.ToDictionary();
            Assert.NotNull(dictionary);
            Assert.Single(dictionary);
            Assert.Equal(obj.Id, dictionary[Base.IdName]);
        }

        private static BaseImplementation Init()
        {
            return new BaseImplementation(Guid.NewGuid().ToString());
        }
    }
}
