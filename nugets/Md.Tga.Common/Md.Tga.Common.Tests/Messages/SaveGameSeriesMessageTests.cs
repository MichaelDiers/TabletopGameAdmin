namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Tests.Models;
    using Xunit;

    public class SaveGameSeriesMessageTests
    {
        [Fact]
        public void Ctor()
        {
            var processId = Guid.NewGuid().ToString();
            var gameSeries = GameSeriesTests.Create();
            var message = new SaveGameSeriesMessage(processId, gameSeries);
            Assert.Equal(processId, message.ProcessId);
            Assert.Equal(gameSeries.Id, message.GameSeries.Id);

            var iMessage = (ISaveGameSeriesMessage) message;
            Assert.Equal(processId, iMessage.ProcessId);
            Assert.Equal(gameSeries.Id, iMessage.GameSeries.Id);
        }
    }
}
