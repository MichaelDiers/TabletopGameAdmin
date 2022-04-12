namespace Md.Tga.Common.Firestore.Tests.Logic
{
    using Md.Common.Contracts.Model;
    using Md.Common.Model;
    using Md.Tga.Common.Firestore.Logic;
    using Xunit;

    public class TestDataReadOnlyDatabaseTests
    {
        [Theory(Skip = "Integration")]
        [InlineData("projectId")]
        public async void ReadStartGameSeriesMessageAsync(string projectId)
        {
            var database = new TestDataReadOnlyDatabase(
                new RuntimeEnvironment {Environment = Environment.Test, ProjectId = projectId});
            var message = await database.ReadStartGameSeriesMessageAsync();
            Assert.NotNull(message);
        }
    }
}
