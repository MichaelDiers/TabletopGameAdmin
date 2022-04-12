namespace Md.Tga.Common.TestData.Tests.Generators
{
    using Md.Tga.Common.TestData.Generators;
    using Xunit;

    public class TestDataContainerTests
    {
        [Fact]
        public void Ctor()
        {
            var testData = new TestDataContainer();
            Assert.NotNull(testData);
        }
    }
}
