namespace Md.Tga.Common.Contracts.Models.MultiLanguage
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Translations for different languages.
    /// </summary>
    public interface ITranslations : IToDictionary
    {
        /// <summary>
        ///     Gets the german translations.
        /// </summary>
        ITranslation German { get; }
    }
}
