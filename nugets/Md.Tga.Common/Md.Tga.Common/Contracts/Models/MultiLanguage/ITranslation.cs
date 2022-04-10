namespace Md.Tga.Common.Contracts.Models.MultiLanguage
{
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Describes the translations for a specific language.
    /// </summary>
    public interface ITranslation : IToDictionary
    {
        /// <summary>
        ///     Gets the translations for a new game survey.
        /// </summary>
        INewGameSurveyTranslations NewGameSurvey { get; }
    }
}
