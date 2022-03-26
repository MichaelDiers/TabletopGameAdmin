namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Models.MultiLanguage;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Models.MultiLanguage;

    /// <summary>
    ///     ReadOnly database for <see cref="Translations" /> collections,
    /// </summary>
    public class TranslationsReadOnlyDatabase : ReadonlyDatabase<ITranslations>, ITranslationsReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the database collection.
        /// </summary>
        public const string CollectionName = "translations";

        /// <summary>
        ///     Creates a new instance of <see cref="GameSeriesReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public TranslationsReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, TranslationsReadOnlyDatabase.CollectionName, Translations.FromDictionary)
        {
        }
    }
}
