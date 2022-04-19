namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Xunit;

    public class SaveGameTerminationSurveyMessageTests
    {
        [Fact]
        public void Json()
        {
            var expected = new SaveGameTerminationSurveyMessage(
                Guid.NewGuid().ToString(),
                new GameTerminationSurvey(
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString()));
            var actual =
                Serializer.DeserializeObject<SaveGameTerminationSurveyMessage>(Serializer.SerializeObject(expected)) as
                    ISaveGameTerminationSurveyMessage;

            Assert.Equal(expected.ProcessId, actual.ProcessId);
            Assert.Equal(expected.GameTerminationSurvey.DocumentId, actual.GameTerminationSurvey.DocumentId);
            Assert.Equal(expected.GameTerminationSurvey.Created, actual.GameTerminationSurvey.Created);
            Assert.Equal(
                expected.GameTerminationSurvey.ParentDocumentId,
                actual.GameTerminationSurvey.ParentDocumentId);
            Assert.Equal(expected.GameTerminationSurvey.WinningSideId, actual.GameTerminationSurvey.WinningSideId);
        }
    }
}
