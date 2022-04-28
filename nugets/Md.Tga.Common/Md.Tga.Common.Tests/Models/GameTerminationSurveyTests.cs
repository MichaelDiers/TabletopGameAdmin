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
            var expected = new GameTerminationResult(
                Guid.NewGuid().ToString(),
                DateTime.Now,
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                "A reason",
                11);
            var actual = GameTerminationResult.FromDictionary(expected.ToDictionary());

            Assert.Equal(expected.DocumentId, actual.DocumentId);
            Assert.Equal(expected.Created, actual.Created);
            Assert.Equal(expected.ParentDocumentId, actual.ParentDocumentId);
            Assert.Equal(expected.PlayerId, actual.PlayerId);
            Assert.Equal(expected.WinningSideId, actual.WinningSideId);
            Assert.Equal(expected.Reason, actual.Reason);
            Assert.Equal(expected.Rounds, actual.Rounds);
        }
    }
}
