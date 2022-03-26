namespace Md.Tga.Common.Firestore.Tests.Logic
{
    using Md.Common.Contracts;
    using Md.Common.Model;
    using Md.Tga.Common.Firestore.Logic;

    /// <summary>
    ///     Tests for <see cref="TranslationsReadOnlyDatabase" />
    /// </summary>
    public class TranslationsReadOnlyDatabaseTests
    {
        // [Theory]
        public async void ReadByDocumentIdAsync(string projectId, string documentId)
        {
            var database = new TranslationsReadOnlyDatabase(
                new RuntimeEnvironment {Environment = Environment.Test, ProjectId = projectId});
            var translations = await database.ReadByDocumentIdAsync(documentId);
        }
    }
}
