namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models.MultiLanguage;

    /// <summary>
    ///     Database operations on translations collection.
    /// </summary>
    public interface ITranslationsReadOnlyDatabase : IReadOnlyDatabase<ITranslations>
    {
    }
}
