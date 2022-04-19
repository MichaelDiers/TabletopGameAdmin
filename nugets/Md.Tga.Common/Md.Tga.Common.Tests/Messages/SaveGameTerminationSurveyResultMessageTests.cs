namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using Md.Common.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Xunit;

    public class SaveGameTerminationSurveyResultMessageTests
    {
        [Fact]
        public void Json()
        {
            var expected = new SaveGameTerminationSurveyResultMessage(
                Guid.NewGuid().ToString(),
                new GameTerminationSurveyResult(
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    true));
            var actual =
                Serializer.DeserializeObject<SaveGameTerminationSurveyResultMessage>(
                    Serializer.SerializeObject(expected)) as ISaveGameTerminationSurveyResultMessage;

            Assert.Equal(expected.ProcessId, actual.ProcessId);
            Assert.Equal(
                expected.GameTerminationSurveyResult.DocumentId,
                actual.GameTerminationSurveyResult.DocumentId);
            Assert.Equal(expected.GameTerminationSurveyResult.Created, actual.GameTerminationSurveyResult.Created);
            Assert.Equal(
                expected.GameTerminationSurveyResult.ParentDocumentId,
                actual.GameTerminationSurveyResult.ParentDocumentId);
            Assert.Equal(expected.GameTerminationSurveyResult.PlayerId, actual.GameTerminationSurveyResult.PlayerId);
            Assert.Equal(expected.GameTerminationSurveyResult.Accept, actual.GameTerminationSurveyResult.Accept);
        }
    }
}
