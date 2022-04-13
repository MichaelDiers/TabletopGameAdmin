namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.Common.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.TestData.Generators;
    using Xunit;

    public class StartSurveyMessageTests
    {
        [Fact]
        public void Json()
        {
            var container = new TestDataContainer();
            var message = new StartSurveyMessage(Guid.NewGuid().ToString(), container.GameSeries, container.Game);
            var fromJson = Serializer.DeserializeObject<StartSurveyMessage>(Serializer.SerializeObject(message));

            Assert.Equal(message.ProcessId, fromJson.ProcessId);
            Assert.Equal(message.GameSeries.DocumentId, fromJson.GameSeries.DocumentId);
            Assert.Equal(message.GameSeries.Created, fromJson.GameSeries.Created);
            Assert.Equal(message.GameSeries.ParentDocumentId, fromJson.GameSeries.ParentDocumentId);
            Assert.Equal(message.GameSeries.GameName, fromJson.GameSeries.GameName);
            Assert.Equal(message.GameSeries.Name, fromJson.GameSeries.Name);
            Assert.Equal(message.GameSeries.ExternalId, fromJson.GameSeries.ExternalId);
            Assert.Equal(message.GameSeries.GameType, fromJson.GameSeries.GameType);
            Assert.Equal(message.GameSeries.Organizer.Email, fromJson.GameSeries.Organizer.Email);
            Assert.Equal(message.GameSeries.Organizer.Name, fromJson.GameSeries.Organizer.Name);
            Assert.Equal(message.GameSeries.Organizer.Id, fromJson.GameSeries.Organizer.Id);

            Assert.Equal(message.Game.Name, fromJson.Game.Name);
            Assert.Equal(message.Game.DocumentId, fromJson.Game.DocumentId);
            Assert.Equal(message.Game.Created, fromJson.Game.Created);
            Assert.Equal(message.Game.ParentDocumentId, fromJson.Game.ParentDocumentId);
        }
    }
}
