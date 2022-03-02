namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.GoogleCloud.Base.Contracts.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Tests.Models;
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
