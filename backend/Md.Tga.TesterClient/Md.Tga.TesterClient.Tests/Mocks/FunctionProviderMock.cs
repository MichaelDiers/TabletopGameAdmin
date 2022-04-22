namespace Md.Tga.TesterClient.Tests.Mocks
{
    using System.Threading.Tasks;

    internal class FunctionProviderMock : IFunctionProvider
    {
        public Task<string> InitializeGameSeries()
        {
            return Task.FromResult(string.Empty);
        }
    }
}
