namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Xunit;

    public class SaveGameStatusMessageTests
    {
        [Fact]
        public void Json()
        {
            var expected = new SaveGameStatusMessage(
                Guid.NewGuid().ToString(),
                new GameStatus(
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
                    Guid.NewGuid().ToString(),
                    Status.Closed));
            var actual =
                Serializer.DeserializeObject<SaveGameStatusMessage>(Serializer.SerializeObject(expected)) as
                    ISaveGameStatusMessage;

            Assert.Equal(expected.ProcessId, actual.ProcessId);
            Assert.Equal(expected.GameStatus.DocumentId, actual.GameStatus.DocumentId);
            Assert.Equal(expected.GameStatus.Created, actual.GameStatus.Created);
            Assert.Equal(expected.GameStatus.ParentDocumentId, actual.GameStatus.ParentDocumentId);
            Assert.Equal(expected.GameStatus.Status, actual.GameStatus.Status);
        }
    }
}
