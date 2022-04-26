namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Xunit;

    public class SaveGameTerminationResultMessageTests
    {
        [Fact]
        public void Json()
        {
            var expected = new SaveGameTerminationResultMessage(
                Guid.NewGuid().ToString(),
                new GameTerminationResult(
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    "A reason."));
            var actual =
                Serializer.DeserializeObject<SaveGameTerminationResultMessage>(Serializer.SerializeObject(expected)) as
                    ISaveGameTerminationResultMessage;

            Assert.Equal(expected.ProcessId, actual.ProcessId);
            Assert.Equal(expected.GameTerminationResult.DocumentId, actual.GameTerminationResult.DocumentId);
            Assert.Equal(expected.GameTerminationResult.Created, actual.GameTerminationResult.Created);
            Assert.Equal(
                expected.GameTerminationResult.ParentDocumentId,
                actual.GameTerminationResult.ParentDocumentId);
            Assert.Equal(expected.GameTerminationResult.PlayerId, actual.GameTerminationResult.PlayerId);
            Assert.Equal(expected.GameTerminationResult.WinningSideId, actual.GameTerminationResult.WinningSideId);
            Assert.Equal(expected.GameTerminationResult.Reason, actual.GameTerminationResult.Reason);
        }
    }
}
