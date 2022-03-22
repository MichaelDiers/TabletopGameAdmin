namespace Md.Tga.InitializeGameSeriesSubscriber.Tests.Data
{
    using System.IO;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts.Messages;

    internal static class TestData
    {
        public static IGameSeries CreateGameSeries()
        {
            var json = File.ReadAllText("Data/game-series.json");
            return JsonConvert.DeserializeObject<GameSeries>(json);
        }

        public static ISurveyClosedMessage Initialize()
        {
            var json = File.ReadAllText("Data/SurveyClosedMessage.json");
            return JsonConvert.DeserializeObject<ISurveyClosedMessage>(json);
        }
    }
}
