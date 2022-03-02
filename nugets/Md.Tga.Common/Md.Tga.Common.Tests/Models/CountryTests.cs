namespace Md.Tga.Common.Tests.Models
{
    using System;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Country" />.
    /// </summary>
    public class CountryTests
    {
        /// <summary>
        ///     Checks for implemented interfaces.
        /// </summary>
        [Fact]
        public void Implements()
        {
            TestHelper.Implements<Country, IBase, INamedBase, ICountry>(
                new Country(Guid.NewGuid().ToString(), "name", Guid.NewGuid().ToString()));
        }
    }
}
