namespace Md.Tga.StartGameSeriesSubscriber.Tests.Mocks
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloudFunctions.Contracts.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Xunit;

    internal class FunctionProviderMock : IPubSubProvider<IStartGameSeriesMessage>
    {
        private readonly IStartGameSeriesMessage expectedMessage;

        public FunctionProviderMock(IStartGameSeriesMessage expectedMessage)
        {
            this.expectedMessage = expectedMessage;
        }

        public Task HandleAsync(IStartGameSeriesMessage message)
        {
            if (this.expectedMessage != null)
            {
                Assert.Equal(this.expectedMessage.ProcessId, message.ProcessId);
                Assert.Equal(this.expectedMessage.GameSeries.ExternalId, message.GameSeries.ExternalId);
                Assert.Equal(this.expectedMessage.GameSeries.GameType, message.GameSeries.GameType);
                Assert.Equal(this.expectedMessage.GameSeries.Name, message.GameSeries.Name);
                Assert.Equal(this.expectedMessage.GameSeries.Organizer.Email, message.GameSeries.Organizer.Email);
                Assert.Equal(this.expectedMessage.GameSeries.Organizer.Name, message.GameSeries.Organizer.Name);
                foreach (var startGameSeriesPerson in this.expectedMessage.GameSeries.Players)
                {
                    Assert.Contains(
                        message.GameSeries.Players,
                        player => player.Email == startGameSeriesPerson.Email &&
                                  player.Name == startGameSeriesPerson.Name);
                }
            }

            return Task.CompletedTask;
        }

        public Task LogErrorAsync(Exception ex, string message)
        {
            return Task.CompletedTask;
        }
    }
}
