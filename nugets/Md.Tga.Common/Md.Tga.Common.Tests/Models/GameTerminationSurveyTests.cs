namespace Md.Tga.Common.Tests.Models
{
    using System;
    using Md.Tga.Common.Models;
    using Xunit;

    public class GameTerminationSurveyTests
    {
        [Fact]
        public void FromToDictionary()
        {
            var expected = new GameTerminationSurvey(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());
            var actual = GameTerminationSurvey.FromDictionary(expected.ToDictionary());

            Assert.Equal(expected.DocumentId, actual.DocumentId);
            Assert.Equal(expected.Created, actual.Created);
            Assert.Equal(expected.ParentDocumentId, actual.ParentDocumentId);
            Assert.Equal(expected.WinningSideId, actual.WinningSideId);
        }
    }
}
