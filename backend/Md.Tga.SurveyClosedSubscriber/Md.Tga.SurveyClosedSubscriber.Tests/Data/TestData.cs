namespace Md.Tga.SurveyClosedSubscriber.Tests.Data
{
    using System;
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
            var dictionary = JsonConvert.DeserializeObject<Game>(json).ToDictionary();
            dictionary.Add(Base.InternalDocumentIdName, Guid.NewGuid().ToString());
            return Game.FromDictionary(dictionary);
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
