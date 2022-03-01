namespace Md.Tga.SaveGameSeriesSubscriber.Tests.Data
{
    using System.IO;
    using Md.TabletopGameAdmin.Common.Messages;
    using Newtonsoft.Json;

    internal static class TestData
    {
        public static SaveGameSeriesMessage SaveGameSeriesMessage()
        {
            var json = File.ReadAllText("Data/SaveGameSeriesMessage.json");
            return JsonConvert.DeserializeObject<SaveGameSeriesMessage>(json);
        }
    }
}
