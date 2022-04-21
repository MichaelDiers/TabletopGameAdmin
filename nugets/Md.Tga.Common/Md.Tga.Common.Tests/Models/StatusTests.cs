namespace Md.Tga.Common.Tests.Models
{
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Xunit;

    public class StatusTests
    {
        [Theory]
        [InlineData(Status.None, "None")]
        [InlineData(Status.Closed, "CLOSED")]
        public void Json(Status status, string expected)
        {
            Assert.Equal($"\"{expected}\"", Serializer.SerializeObject(status));
        }
    }
}
