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
            var gameSeriesId = Guid.NewGuid().ToString();

            var message = new StartGameMessage(processId, gameSeriesId) as IStartGameMessage;

            Assert.Equal(processId, message.ProcessId);
            Assert.Equal(gameSeriesId, message.InternalGameSeriesId);
        }
    }
}
