namespace Md.Tga.SurveyClosedSubscriber.Tests.Data
{
    using System.IO;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;

    internal static class TestData
    {
        public static Game CreateGame()
        {
            var json = File.ReadAllText("Data/game.json");
            return JsonConvert.DeserializeObject<Game>(json);
        }

        public static GameSeries CreateGameSeries()
        {
            var json = File.ReadAllText("Data/game-series.json");
            return JsonConvert.DeserializeObject<GameSeries>(json);
        }

        public static ISurveyClosedMessage Initialize()
        {
            var json = File.ReadAllText("Data/survey-closed-message.json");
            return JsonConvert.DeserializeObject<SurveyClosedMessage>(json);
        }
    }
}
