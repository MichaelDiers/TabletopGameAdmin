namespace Md.Tga.Common.TestData.Generators
{
    using System;

    public class GameGeneratorConfiguration : BaseGeneratorConfiguration
    {
        public string Name { get; set; } = $"GameName-{Guid.NewGuid().ToString()}";
    }
}
