namespace Md.Tga.Common.Firestore.Tests.Logic
{
    using Md.Common.Contracts.Model;
    using Md.Common.Model;
    using Md.Tga.Common.Firestore.Logic;
    using Xunit;

    public class GameConfigReadOnlyDatabaseTests
    {
        [Theory(Skip = "Integration")]
        [InlineData("projectId", "key")]
        public async void ReadByDocumentIdAsync(string projectId, string documentId)
        {
            var database = new GameConfigReadOnlyDatabase(
                new RuntimeEnvironment {Environment = Environment.Test, ProjectId = projectId});
            var data = await database.ReadByDocumentIdAsync(documentId);
            Assert.NotNull(data);
        }
    }
}
