namespace Md.Tga.SaveGameSubscriber.Tests.Data
{
    using System.IO;
    using Md.Tga.Common.Messages;
    using Newtonsoft.Json;

    internal static class TestData
    {
        public static SaveGameMessage SaveGameMessage()
        {
            var json = File.ReadAllText("Data/SaveGameMessage.json");
            return JsonConvert.DeserializeObject<SaveGameMessage>(json);
        }
    }
}
