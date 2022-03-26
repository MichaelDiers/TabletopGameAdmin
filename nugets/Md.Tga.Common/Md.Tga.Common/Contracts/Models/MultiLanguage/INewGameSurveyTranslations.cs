namespace Md.Tga.Common.Contracts.Models.MultiLanguage
{
    using System.Collections.Generic;

    /// <summary>
    ///     Specifies game translations.
    /// </summary>
    public interface INewGameSurveyTranslations
    {
        /// <summary>
        ///     Gets the default answer for survey questions.
        /// </summary>
        string AnswerDefault { get; }

        /// <summary>
        ///     Gets the name of the game.
        /// </summary>
        string GameName { get; }

        /// <summary>
        ///     Gets the survey questions.
        /// </summary>
        IEnumerable<string> Questions { get; }

        /// <summary>
        ///     Gets an info text for the survey.
        /// </summary>
        string SurveyInfo { get; }

        /// <summary>
        ///     Gets an info link for the survey.
        /// </summary>
        string SurveyInfoLink { get; }
    }
}
