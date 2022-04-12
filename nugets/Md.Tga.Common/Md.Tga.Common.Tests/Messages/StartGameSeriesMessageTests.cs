namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using System.Linq;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="StartGameSeriesMessage" />.
    /// </summary>
    public class StartGameSeriesMessageTests
    {
        [Fact]
        public void Ctor()
        {
            var processId = Guid.NewGuid().ToString();
            var externalId = Guid.NewGuid().ToString();
            const string gameName = "name organizer";
            const string gameType = "gameType";
            const string organizerName = "name";
            const string organizerEmail = "organizer@example.example";
            const string playerName = "name";
            const string playerEmail = "player@example.example";
            var message = new StartGameSeriesMessage(
                processId,
                new StartGameSeries(
                    externalId,
                    gameName,
                    gameType,
                    new StartGameSeriesPerson(organizerName, organizerEmail),
                    new[] {new StartGameSeriesPerson(playerName, playerEmail)}));
            var iMessage = (IStartGameSeriesMessage) message;
            Assert.Equal(processId, iMessage.ProcessId);
            Assert.Equal(externalId, iMessage.GameSeries.ExternalId);
            Assert.Equal(gameName, iMessage.GameSeries.Name);
            Assert.Equal(gameType, iMessage.GameSeries.GameType);
            Assert.Equal(organizerName, iMessage.GameSeries.Organizer.Name);
            Assert.Equal(organizerEmail, iMessage.GameSeries.Organizer.Email);
            Assert.Equal(playerName, iMessage.GameSeries.Players.Single().Name);
            Assert.Equal(playerEmail, iMessage.GameSeries.Players.Single().Email);
        }
    }
}
