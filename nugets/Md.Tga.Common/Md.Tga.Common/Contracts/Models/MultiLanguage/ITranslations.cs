namespace Md.Tga.Common.Contracts.Models.MultiLanguage
{
    /// <summary>
    ///     Translations for different languages.
    /// </summary>
    public interface ITranslations
    {
        /// <summary>
        ///     Gets the german translations.
        /// </summary>
        ITranslation German { get; }
    }
}
