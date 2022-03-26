namespace Md.Tga.StartGameSubscriber.Tests.Mocks
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Xunit;

    internal class FunctionProviderMock : IPubSubProvider<IStartGameMessage>
    {
        private readonly IStartGameMessage expectedMessage;

        public FunctionProviderMock(IStartGameMessage expectedMessage)
        {
            this.expectedMessage = expectedMessage;
        }

        public Task HandleAsync(IStartGameMessage message)
        {
            Assert.NotNull(message);
            Assert.Equal(this.expectedMessage.ProcessId, message.ProcessId);
            Assert.Equal(this.expectedMessage.InternalGameSeriesId, message.InternalGameSeriesId);

            return Task.CompletedTask;
        }

        public Task LogErrorAsync(Exception ex, string message)
        {
            return Task.CompletedTask;
        }
    }
}
