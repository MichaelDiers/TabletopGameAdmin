namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Tests.Models;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="SaveGameMessage" />.
    /// </summary>
    public class SaveGameMessageTests
    {
        [Fact]
        public void Implements()
        {
            var game = GameTests.Init();
            var message = new SaveGameMessage(Guid.NewGuid().ToString(), game);

            TestHelper.Implements<SaveGameMessage, ISaveGameMessage, IMessage>(message);
        }

        [Fact]
        public void Serialize()
        {
            var game = GameTests.Init();
            var message = new SaveGameMessage(Guid.NewGuid().ToString(), game);

            var actual = JsonConvert.SerializeObject(message);
            Assert.Equal(SaveGameMessageTests.SerializePlain(message), actual);

            actual = JsonConvert.SerializeObject(message);
            Assert.Equal(SaveGameMessageTests.SerializePlain(message), actual);
        }

        private static string SerializePlain(ISaveGameMessage obj)
        {
            var game = GameTests.SerializePlain(obj.Game);

            return $"{{\"processId\":\"{obj.ProcessId}\",\"game\":{game}}}";
        }
    }
}
