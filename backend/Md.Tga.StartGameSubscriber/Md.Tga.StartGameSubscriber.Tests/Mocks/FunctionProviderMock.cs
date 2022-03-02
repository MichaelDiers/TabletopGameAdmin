﻿namespace Md.Tga.StartGameSubscriber.Tests.Mocks
{
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.TabletopGameAdmin.Common.Contracts.Messages;
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
            Assert.Equal(this.expectedMessage.ProcessId, message.ProcessId);
            if (this.expectedMessage.GameSeries == null)
            {
                Assert.Null(message.GameSeries);
            }
            else
            {
                Assert.Equal(this.expectedMessage.GameSeries.Id, message.GameSeries.Id);
                Assert.Equal(this.expectedMessage.GameSeries.Name, message.GameSeries.Name);
                Assert.Equal(this.expectedMessage.GameSeries.Sides.Count(), message.GameSeries.Sides.Count());
                foreach (var gameSeriesSide in this.expectedMessage.GameSeries.Sides)
                {
                    Assert.Contains(
                        message.GameSeries.Sides,
                        side => side.Id == gameSeriesSide.Id && side.Name == gameSeriesSide.Name);
                }

                Assert.Equal(this.expectedMessage.GameSeries.Countries.Count(), message.GameSeries.Countries.Count());
                foreach (var gameSeriesCountry in this.expectedMessage.GameSeries.Countries)
                {
                    Assert.Contains(
                        message.GameSeries.Countries,
                        country => country.Id == gameSeriesCountry.Id
                                   && country.Name == gameSeriesCountry.Name
                                   && country.SideId == gameSeriesCountry.SideId);
                }

                Assert.Equal(this.expectedMessage.GameSeries.Players.Count(), message.GameSeries.Players.Count());
                foreach (var gameSeriesPlayer in this.expectedMessage.GameSeries.Players)
                {
                    Assert.Contains(
                        message.GameSeries.Players,
                        player => player.Id == gameSeriesPlayer.Id
                                  && player.Name == gameSeriesPlayer.Name
                                  && player.Email == gameSeriesPlayer.Email);
                }

                Assert.Equal(this.expectedMessage.GameSeries.Organizer.Email, message.GameSeries.Organizer.Email);
                Assert.Equal(this.expectedMessage.GameSeries.Organizer.Id, message.GameSeries.Organizer.Id);
                Assert.Equal(this.expectedMessage.GameSeries.Organizer.Name, message.GameSeries.Organizer.Name);
            }

            return Task.CompletedTask;
        }
    }
}