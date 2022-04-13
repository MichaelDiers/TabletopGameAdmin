namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.TestData.Generators;
    using Md.Tga.Common.Tests.Models;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="SaveGameMessage" />.
    /// </summary>
    public class SaveGameMessageTests
    {
        [Fact(Skip = "pending")]
        public void Implements()
        {
            var testData = new TestDataContainer();
            var message = new SaveGameMessage(Guid.NewGuid().ToString(), testData.GameSeries, testData.Game);
            TestHelper.Implements<SaveGameMessage, ISaveGameMessage, IMessage>(message);
        }

        [Fact(Skip = "pending")]
        public void Serialize()
        {
            var testData = new TestDataContainer();
            var message = new SaveGameMessage(Guid.NewGuid().ToString(), testData.GameSeries, testData.Game);

            var actual = JsonConvert.SerializeObject(message);
            Assert.Equal(SaveGameMessageTests.SerializePlain(message), actual);

            actual = JsonConvert.SerializeObject(message);
            Assert.Equal(SaveGameMessageTests.SerializePlain(message), actual);
        }

        private static string SerializePlain(ISaveGameMessage obj)
        {
            var game = GameTests.SerializePlain(obj.Game);
            var gameSeries = GameSeriesTests.SerializePlain(obj.GameSeries);
            return $"{{\"processId\":\"{obj.ProcessId}\",\"game\":{game},\"gameSeries\":{gameSeries}}}";
        }
    }
}
