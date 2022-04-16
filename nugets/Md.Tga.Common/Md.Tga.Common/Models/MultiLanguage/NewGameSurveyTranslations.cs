namespace Md.Tga.Common.Models.MultiLanguage
{
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Extensions;
    using Md.Common.Model;
    using Md.Tga.Common.Contracts.Models.MultiLanguage;

    /// <summary>
    ///     Specifies game translations.
    /// </summary>
    public class NewGameSurveyTranslations : ToDictionaryConverter, INewGameSurveyTranslations
    {
        /// <summary>
        ///     The database name for <see cref="AnswerDefault" />.
        /// </summary>
        private const string AnswersDefaultName = "answer-default";

        /// <summary>
        ///     The database name for <see cref="GameName" />.
        /// </summary>
        private const string GameNameName = "game-name";

        /// <summary>
        ///     The database name for <see cref="Questions" />.
        /// </summary>
        private const string QuestionsName = "questions";

        /// <summary>
        ///     The database name for <see cref="SurveyInfoLink" />.
        /// </summary>
        private const string SurveyInfoLinkName = "survey-info-link";

        /// <summary>
        ///     The database name for <see cref="SurveyInfo" />.
        /// </summary>
        private const string SurveyInfoName = "survey-info";

        /// <summary>
        ///     Creates a new instance of <see cref="NewGameSurveyTranslations" />.
        /// </summary>
        /// <param name="answerDefault">The default answer for survey questions.</param>
        /// <param name="gameName"></param>
        /// <param name="questions"></param>
        /// <param name="surveyInfo"></param>
        /// <param name="surveyInfoLink"></param>
        public NewGameSurveyTranslations(
            string answerDefault,
            string gameName,
            IEnumerable<string> questions,
            string surveyInfo,
            string surveyInfoLink
        )
        {
            this.AnswerDefault = answerDefault.ValidateIsNotNullOrWhitespace(nameof(answerDefault));
            this.GameName = gameName.ValidateIsNotNullOrWhitespace(nameof(gameName));
            this.Questions = questions.Select(x => x.ValidateIsNotNullOrWhitespace(nameof(questions)));
            this.SurveyInfo = surveyInfo.ValidateIsNotNullOrWhitespace(nameof(surveyInfo));
            this.SurveyInfoLink = surveyInfoLink.ValidateIsNotNullOrWhitespace(nameof(surveyInfoLink));
        }

        /// <summary>
        ///     Gets the default answer for survey questions.
        /// </summary>
        public string AnswerDefault { get; }

        /// <summary>
        ///     Gets the name of the game.
        /// </summary>
        public string GameName { get; }

        /// <summary>
        ///     Gets the survey questions.
        /// </summary>
        public IEnumerable<string> Questions { get; }

        /// <summary>
        ///     Gets an info text for the survey.
        /// </summary>
        public string SurveyInfo { get; }

        /// <summary>
        ///     Gets an info link for the survey.
        /// </summary>
        public string SurveyInfoLink { get; }

        /// <summary>
        ///     Add object values to dictionary.
        /// </summary>
        /// <param name="dictionary">The data is added to this dictionary.</param>
        /// <returns>The dictionary given as parameter.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(NewGameSurveyTranslations.AnswersDefaultName, this.AnswerDefault);
            dictionary.Add(NewGameSurveyTranslations.GameNameName, this.GameName);
            dictionary.Add(NewGameSurveyTranslations.QuestionsName, this.Questions.ToArray());
            dictionary.Add(NewGameSurveyTranslations.SurveyInfoName, this.SurveyInfo);
            dictionary.Add(NewGameSurveyTranslations.SurveyInfoLinkName, this.SurveyInfoLink);
            return dictionary;
        }

        /// <summary>
        ///     Creates a new <see cref="INewGameSurveyTranslations" /> from dictionary data.
        /// </summary>
        /// <param name="dictionary">The dictionary data from database.</param>
        /// <returns>An <see cref="INewGameSurveyTranslations" />.</returns>
        public static INewGameSurveyTranslations FromDictionary(IDictionary<string, object> dictionary)
        {
            return new NewGameSurveyTranslations(
                dictionary.GetString(NewGameSurveyTranslations.AnswersDefaultName),
                dictionary.GetString(NewGameSurveyTranslations.GameNameName),
                dictionary.GetEnumerableOfString(NewGameSurveyTranslations.QuestionsName).ToArray(),
                dictionary.GetString(NewGameSurveyTranslations.SurveyInfoName),
                dictionary.GetString(NewGameSurveyTranslations.SurveyInfoLinkName));
        }
    }
}
