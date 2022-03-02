namespace Md.Tga.Common.Tests.Models
{
    using System;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Person" />.
    /// </summary>
    public class PersonTests
    {
        /// <summary>
        ///     Checks for implemented interfaces.
        /// </summary>
        [Fact]
        public void Implements()
        {
            TestHelper.Implements<Person, IBase, INamedBase, IPerson>(
                new Person(Guid.NewGuid().ToString(), "name", "person@foo.example"));
        }
    }
}
