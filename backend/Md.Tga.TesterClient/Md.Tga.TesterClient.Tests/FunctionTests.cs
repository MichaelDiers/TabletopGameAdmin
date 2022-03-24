namespace Md.Tga.TesterClient.Tests
{
    using System.IO;
    using System.Threading.Tasks;
    using Md.Tga.TesterClient.Tests.Mocks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging.Abstractions;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="Function" />.
    /// </summary>
    public class FunctionTests
    {
        [Fact]
        public async void Get()
        {
            var context = new DefaultHttpContext();
            var text = await FunctionTests.ExecuteRequest(context);
            Assert.True(string.IsNullOrWhiteSpace(text));
        }

        private static async Task<string> ExecuteRequest(HttpContext context)
        {
            var responseStream = new MemoryStream();
            context.Response.Body = responseStream;

            var function = new Function(new NullLogger<Function>(), new FunctionProviderMock());
            await function.HandleAsync(context);
            Assert.Equal(200, context.Response.StatusCode);
            await context.Response.BodyWriter.CompleteAsync();
            context.Response.Body.Position = 0;

            TextReader reader = new StreamReader(responseStream);
            return await reader.ReadToEndAsync();
        }
    }
}
