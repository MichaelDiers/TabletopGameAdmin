namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using System.Linq;
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.TestData.Generators;
    using Xunit;

    public class CreateGameMailMessageTests
    {
        [Fact]
        public void Serialize()
        {
            var container = new TestDataContainer();
            var message = new CreateGameMailMessage(
                Guid.NewGuid().ToString(),
                GameMailType.SurveyResult,
                container.GameSeries,
                container.Game,
                new PlayerMappings(
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
                    Guid.NewGuid().ToString(),
                    Enumerable.Empty<IPlayerCountryMapping>()),
                new[]
                {
                    new GameTerminationResult(
                        Guid.NewGuid().ToString(),
                        DateTime.Now,
                        container.Game.DocumentId,
                        container.GameSeries.Players.First().Id,
                        container.GameSeries.Sides.First().Id)
                });
            var actual =
                (ICreateGameMailMessage) Serializer.DeserializeObject<CreateGameMailMessage>(
                    Serializer.SerializeObject(message));
            Assert.Equal(message.GameMailType, actual.GameMailType);
            Assert.Equal(message.GameSeries.Name, actual.GameSeries.Name);
            Assert.Equal(message.PlayerMappings.DocumentId, actual.PlayerMappings.DocumentId);
        }
    }
}
