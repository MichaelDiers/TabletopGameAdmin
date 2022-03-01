namespace Md.Tga.InitializeGameSeriesSubscriber.Tests.Data
{
    using System.IO;
    using Md.TabletopGameAdmin.Common.Messages;
    using Newtonsoft.Json;

    internal static class TestData
    {
        public static InitializeGameSeriesMessage InitializeGameSeriesMessage()
        {
            var json = File.ReadAllText("Data/InitializeGameSeriesMessage.json");
            return JsonConvert.DeserializeObject<InitializeGameSeriesMessage>(json);
        }
    }
}
