namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Xunit;

    public class StartGameMessageTests
    {
        [Fact]
        public void Ctor()
        {
            var processId = Guid.NewGuid().ToString();
            var gameSeriesDocumentId = Guid.NewGuid().ToString();

            var message = new StartGameMessage(processId, gameSeriesDocumentId) as IStartGameMessage;

            Assert.Equal(processId, message.ProcessId);
            Assert.Equal(gameSeriesDocumentId, message.GameSeriesDocumentId);
        }

        [Fact]
        public void CtorArgumentExceptionForInvalidDocumentId()
        {
            Assert.Throws<ArgumentException>(() => new StartGameMessage(Guid.NewGuid().ToString(), string.Empty));
        }

        [Fact]
        public void CtorArgumentExceptionForInvalidProcessId()
        {
            Assert.Throws<ArgumentException>(() => new StartGameMessage(string.Empty, Guid.NewGuid().ToString()));
        }
    }
}
