namespace Md.Tga.StartGameSubscriber.Tests.Data
{
    using System.IO;
    using Md.Tga.Common.Messages;
    using Newtonsoft.Json;

    internal static class TestData
    {
        public static StartGameMessage StartGameMessage()
        {
            var json = File.ReadAllText("Data/StartGameMessage.json");
            return JsonConvert.DeserializeObject<StartGameMessage>(json);
        }

        public static StartGameMessage StartGameMessageWithoutGameSeries()
        {
            var message = StartGameMessage();
            return new StartGameMessage(message.ProcessId, message.InternalId);
        }
    }
}
