namespace Md.Tga.Common.Firestore.Tests.Logic
{
    using Md.Common.Contracts.Model;
    using Md.Common.Model;
    using Md.Tga.Common.Firestore.Logic;
    using Xunit;

    /// <summary>
    ///     Tests for <see cref="TranslationsReadOnlyDatabase" />
    /// </summary>
    public class TranslationsReadOnlyDatabaseTests
    {
        [Theory(Skip = "IntegrationOnly")]
        [InlineData("projectId", "documentId")]
        public async void ReadByDocumentIdAsync(string projectId, string documentId)
        {
            var database = new TranslationsReadOnlyDatabase(
                new RuntimeEnvironment {Environment = Environment.Test, ProjectId = projectId});
            var translations = await database.ReadByDocumentIdAsync(documentId);
            Assert.NotNull(translations);
        }
    }
}
