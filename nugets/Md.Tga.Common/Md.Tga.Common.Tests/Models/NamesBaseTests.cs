namespace Md.Tga.Common.Tests.Models
{
    using System;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="NamedBase" />.
    /// </summary>
    public class NamesBaseTests
    {
        /// <summary>
        ///     Checks for implemented interfaces.
        /// </summary>
        [Fact]
        public void Implements()
        {
            TestHelper.Implements<NamedBase, IBase, INamedBase>(new NamedBase(Guid.NewGuid().ToString(), "name"));
        }
    }
}
