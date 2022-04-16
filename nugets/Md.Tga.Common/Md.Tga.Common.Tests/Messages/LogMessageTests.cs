namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Xunit;

    public class LogMessageTests
    {
        [Fact]
        public void Serialize()
        {
            var expected = new LogMessage(Guid.NewGuid().ToString(), "message", new Exception("foo"));
            var actual = (ILogMessage) Serializer.DeserializeObject<LogMessage>(Serializer.SerializeObject(expected));
            Assert.Equal(expected.ProcessId, actual.ProcessId);
            Assert.Equal(expected.Message, actual.Message);
            Assert.NotNull(expected.Exception);
            Assert.NotNull(actual.Exception);
            Assert.Equal(expected.Exception.Message, actual.Exception.Message);
        }
    }
}
