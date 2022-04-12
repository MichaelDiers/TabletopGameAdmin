namespace Md.Tga.Common.TestData.Generators
{
    using System;

    public class GameSeriesGeneratorConfiguration : BaseGeneratorConfiguration
    {
        public int CountryCount { get; set; } = 5;

        public string ExternalId { get; set; } = Guid.NewGuid().ToString();

        public string GameName { get; set; } = "GameTypeName";
        public string GameType { get; set; } = "GameType";

        public string Name { get; set; } = $"GameSeriesName-{Guid.NewGuid().ToString()}";

        public int PlayerCount { get; set; } = 5;

        public int SideCount { get; set; } = 2;
    }
}
