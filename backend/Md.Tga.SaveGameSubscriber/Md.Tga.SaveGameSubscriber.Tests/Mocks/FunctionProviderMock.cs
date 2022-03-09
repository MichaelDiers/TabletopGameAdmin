namespace Md.Tga.SaveGameSubscriber.Tests.Mocks
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Xunit;

    internal class FunctionProviderMock : IPubSubProvider<ISaveGameMessage>
    {
        private readonly ISaveGameMessage expectedMessage;

        public FunctionProviderMock(ISaveGameMessage expectedMessage)
        {
            this.expectedMessage = expectedMessage;
        }

        public Task HandleAsync(ISaveGameMessage message)
        {
            Assert.Equal(this.expectedMessage.ProcessId, message.ProcessId);
            Assert.Equal(this.expectedMessage.Game.Id, message.Game.Id);
            Assert.Equal(this.expectedMessage.Game.Name, message.Game.Name);
            Assert.Equal(this.expectedMessage.Game.InternalGameSeriesId, message.Game.InternalGameSeriesId);
            Assert.Equal(this.expectedMessage.Game.SurveyId, message.Game.SurveyId);
            return Task.CompletedTask;
        }

        public Task LogErrorAsync(Exception ex, string message)
        {
            return Task.CompletedTask;
        }
    }
}
