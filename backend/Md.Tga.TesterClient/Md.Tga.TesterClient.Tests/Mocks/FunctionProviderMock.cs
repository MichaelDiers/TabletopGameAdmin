namespace Md.Tga.TesterClient.Tests.Mocks
{
    using System.Threading.Tasks;
    using Md.Tga.TesterClient.Contracts;

    internal class FunctionProviderMock : IFunctionProvider
    {
        public Task InitializeGameSeries()
        {
            return Task.CompletedTask;
        }
    }
}
