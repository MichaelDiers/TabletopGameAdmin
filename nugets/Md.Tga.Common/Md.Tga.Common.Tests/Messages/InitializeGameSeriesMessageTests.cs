namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.GoogleCloud.Base.Contracts.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Tests.Models;
    using Newtonsoft.Json;
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

        [Fact]
        public void Serialize()
        {
            var gameSeries = GameSeriesTests.Create();
            var message = new InitializeGameSeriesMessage(Guid.NewGuid().ToString(), gameSeries);

            var actual = JsonConvert.SerializeObject(message);
            Assert.Equal(SerializePlain(message), actual);

            actual = JsonConvert.SerializeObject(message);
            Assert.Equal(SerializePlain(message), actual);
        }

        public static string SerializePlain(IInitializeGameSeriesMessage obj)
        {
            var gameSeries = GameSeriesTests.SerializePlain(obj.GameSeries);
            return $"{{\"processId\":\"{obj.ProcessId}\",\"gameSeries\":{gameSeries}}}";
        }
    }
}
