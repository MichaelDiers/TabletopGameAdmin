namespace Md.Tga.TesterClient.Tests.Mocks
{
    using System.Threading.Tasks;

    internal class FunctionProviderMock : IFunctionProvider
    {
        public Task InitializeGameSeries()
        {
            return Task.CompletedTask;
        }
    }
}
