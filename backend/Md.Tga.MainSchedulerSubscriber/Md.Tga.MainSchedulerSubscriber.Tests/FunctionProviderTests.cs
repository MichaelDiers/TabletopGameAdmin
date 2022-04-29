namespace Md.Tga.MainSchedulerSubscriber.Tests
{
    using Google.Cloud.Functions.Testing;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="FunctionProvider" />
    /// </summary>
    public class FunctionProviderTests
    {
        [Fact]
        public async void HandleAsync()
        {
            var logger = new MemoryLogger<Function>();
            var provider = new FunctionProvider();

            await provider.HandleAsync();

            Assert.Empty(logger.ListLogEntries());
        }
    }
}
