namespace Md.Tga.Common.Contracts.Models.MultiLanguage
{
    /// <summary>
    ///     Describes the translations for a specific language.
    /// </summary>
    public interface ITranslation
    {
        /// <summary>
        ///     Gets the translations for a new game survey.
        /// </summary>
        INewGameSurveyTranslations NewGameSurvey { get; }
    }
}
