namespace TabletopGameAdmin.Common.Tests.Messages
{
    using System;
    using Md.GoogleCloudPubSub.Base.Contracts.Messages;
    using TabletopGameAdmin.Common.Contracts.Messages;
    using TabletopGameAdmin.Common.Messages;
    using TabletopGameAdmin.Common.Tests.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="InitializeGameSeriesMessage" />.
    /// </summary>
    public class InitializeGameSeriesMessageTests
    {
        [Fact]
        public void Implements()
        {
            var gameSeries = GameSeriesTests.Create();
            var message = new InitializeGameSeriesMessage(Guid.NewGuid().ToString(), gameSeries);

            TestHelper.Implements<InitializeGameSeriesMessage, IInitializeGameSeriesMessage, IMessage>(message);
        }
    }
}
