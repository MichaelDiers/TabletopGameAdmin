namespace Md.Tga.Common.Tests.Models
{
    using System;
    using Md.Tga.Common.Models;
    using Xunit;

    public class GameNameTests
    {
        [Fact]
        public void FromToDictionary()
        {
            var expected = new GameName(Guid.NewGuid().ToString(), DateTime.Now, "the name");
            var actual = GameName.FromDictionary(expected.ToDictionary());

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.DocumentId, actual.DocumentId);
            Assert.Equal(expected.Created, actual.Created);
        }
    }
}
