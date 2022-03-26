namespace Md.Tga.Common.Models.MultiLanguage
{
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Models.MultiLanguage;

    /// <summary>
    ///     Translations for a specific language.
    /// </summary>
    public class Translation : ITranslation
    {
        /// <summary>
        ///     Database name for <see cref="NewGameSurvey" />.
        /// </summary>
        private const string NewGameSurveyName = "NewGameSurvey";

        /// <summary>
        ///     Creates a new instance of <see cref="Translation" />
        /// </summary>
        /// <param name="newGameSurveyTranslations">The translations for a new game survey.</param>
        public Translation(INewGameSurveyTranslations newGameSurveyTranslations)
        {
            this.NewGameSurvey = newGameSurveyTranslations;
        }

        /// <summary>
        ///     Gets the translations for a new game survey.
        /// </summary>
        public INewGameSurveyTranslations NewGameSurvey { get; }

        /// <summary>
        ///     Creates a new <see cref="Translation" /> from database data.
        /// </summary>
        /// <param name="dictionary">Dictionary data from the database.</param>
        /// <returns>An <see cref="ITranslation" />.</returns>
        public static ITranslation FromDictionary(IDictionary<string, object> dictionary)
        {
            return new Translation(
                NewGameSurveyTranslations.FromDictionary(dictionary.GetDictionary(Translation.NewGameSurveyName)));
        }
    }
}
