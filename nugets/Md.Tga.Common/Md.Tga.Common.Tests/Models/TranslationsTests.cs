namespace Md.Tga.Common.Tests.Models
{
    using System.Linq;
    using Md.Tga.Common.Models.MultiLanguage;
    using Xunit;

    public class TranslationsTests
    {
        [Fact]
        public void FromDictionary()
        {
            var translations = new Translations(
                new Translation(
                    new NewGameSurveyTranslations(
                        "answer",
                        "gameName",
                        new[] {"foo"},
                        "surveyInfo",
                        "link")));
            var fromDictionary = Translations.FromDictionary(translations.ToDictionary());
            Assert.Equal(translations.German.NewGameSurvey.GameName, fromDictionary.German.NewGameSurvey.GameName);
            Assert.Equal(
                translations.German.NewGameSurvey.AnswerDefault,
                fromDictionary.German.NewGameSurvey.AnswerDefault);
            Assert.Equal(translations.German.NewGameSurvey.SurveyInfo, fromDictionary.German.NewGameSurvey.SurveyInfo);
            Assert.Equal(
                translations.German.NewGameSurvey.SurveyInfoLink,
                fromDictionary.German.NewGameSurvey.SurveyInfoLink);
            Assert.Equal(
                translations.German.NewGameSurvey.Questions.First(),
                fromDictionary.German.NewGameSurvey.Questions.First());
        }
    }
}
