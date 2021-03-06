namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Xunit;

    public class StartGameTerminationMessageTests
    {
        [Fact]
        public void Json()
        {
            var expected = new StartGameTerminationMessage(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                "A reason.",
                7);
            var actual =
                Serializer.DeserializeObject<StartGameTerminationMessage>(Serializer.SerializeObject(expected)) as
                    IStartGameTerminationMessage;

            Assert.Equal(expected.ProcessId, actual.ProcessId);
            Assert.Equal(expected.GameSeriesDocumentId, actual.GameSeriesDocumentId);
            Assert.Equal(expected.GameDocumentId, actual.GameDocumentId);
            Assert.Equal(expected.TerminationId, actual.TerminationId);
            Assert.Equal(expected.WinningSideId, actual.WinningSideId);
            Assert.Equal(expected.Reason, actual.Reason);
            Assert.Equal(expected.Rounds, actual.Rounds);
        }
    }
}
