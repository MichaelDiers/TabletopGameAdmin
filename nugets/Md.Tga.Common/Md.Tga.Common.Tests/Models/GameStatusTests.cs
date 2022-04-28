namespace Md.Tga.Common.Tests.Models
{
    using System;
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Xunit;

    public class GameStatusTests
    {
        [Fact]
        public void FromToDictionary()
        {
            var expected = new GameStatus(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                Guid.NewGuid().ToString(),
                Status.Closed,
                Guid.NewGuid().ToString(),
                0);
            var actual = GameStatus.FromDictionary(expected.ToDictionary());

            Assert.Equal(expected.DocumentId, actual.DocumentId);
            Assert.Equal(expected.Created, actual.Created);
            Assert.Equal(expected.ParentDocumentId, actual.ParentDocumentId);
            Assert.Equal(expected.Status, actual.Status);
            Assert.Equal(expected.WinningSideId, actual.WinningSideId);
            Assert.Equal(expected.Rounds, actual.Rounds);
        }

        [Fact]
        public void Json()
        {
            var expected = new GameStatus(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                Guid.NewGuid().ToString(),
                Status.Closed,
                Guid.NewGuid().ToString(),
                6);
            var actual = Serializer.DeserializeObject<GameStatus>(Serializer.SerializeObject(expected));

            Assert.Equal(expected.DocumentId, actual.DocumentId);
            Assert.Equal(expected.Created, actual.Created);
            Assert.Equal(expected.ParentDocumentId, actual.ParentDocumentId);
            Assert.Equal(expected.Status, actual.Status);
            Assert.Equal(expected.WinningSideId, actual.WinningSideId);
            Assert.Equal(expected.Rounds, actual.Rounds);
        }
    }
}
