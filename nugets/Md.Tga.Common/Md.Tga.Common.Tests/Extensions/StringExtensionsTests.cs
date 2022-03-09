namespace Md.Tga.Common.Tests.Extensions
{
    using System;
    using Md.Tga.Common.Extensions;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="StringExtensions" />.
    /// </summary>
    public class StringExtensionsTests
    {
        [Fact]
        public void GuidIsValid()
        {
            var guid = Guid.NewGuid().ToString();
            Assert.Equal(guid, guid.ValidateIsAGuid());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("wefgh")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void GuidIsInvalid(string guid)
        {
            Assert.Throws<ArgumentException>(guid.ValidateIsAGuid);
        }
    }
}
