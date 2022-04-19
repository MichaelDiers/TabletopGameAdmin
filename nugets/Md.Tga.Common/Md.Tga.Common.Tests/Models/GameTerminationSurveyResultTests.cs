namespace Md.Tga.Common.Tests.Models
{
    using System;
    using Md.Tga.Common.Models;
    using Xunit;

    public class GameTerminationSurveyResultTests
    {
        [Fact]
        public void FromToDictionary()
        {
            var expected = new GameTerminationSurveyResult(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                true);
            var actual = GameTerminationSurveyResult.FromDictionary(expected.ToDictionary());

            Assert.Equal(expected.DocumentId, actual.DocumentId);
            Assert.Equal(expected.Created, actual.Created);
            Assert.Equal(expected.ParentDocumentId, actual.ParentDocumentId);
            Assert.Equal(expected.PlayerId, actual.PlayerId);
            Assert.Equal(expected.Accept, actual.Accept);
        }
    }
}
