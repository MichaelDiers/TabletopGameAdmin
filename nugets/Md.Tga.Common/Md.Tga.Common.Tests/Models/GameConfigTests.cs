namespace Md.Tga.Common.Tests.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Models;
    using Xunit;

    public class GameConfigTests
    {
        [Fact]
        public void Ctor()
        {
            const string gameName = "gameName";
            const string countryName = "countryName";
            const string countrySide = "countrySide";

            var config = new GameConfig(gameName, new[] {new GameCountryConfig(countryName, countrySide)});
            var iConfig = (IGameConfig) config;

            Assert.Equal(gameName, iConfig.Name);
            Assert.Equal(countryName, iConfig.Countries.Single().Name);
            Assert.Equal(countrySide, iConfig.Countries.Single().Side);
        }

        [Fact]
        public void FromDictionary()
        {
            const string gameName = "gameName";
            const string countryName = "countryName";
            const string countrySide = "countrySide";

            var dictionary = new Dictionary<string, object>
            {
                {GameConfig.NameName, gameName},
                {
                    GameConfig.CountriesName,
                    new[]
                    {
                        new Dictionary<string, object>
                        {
                            {GameCountryConfig.NameName, countryName},
                            {GameCountryConfig.SideName, countrySide}
                        }
                    }
                }
            };

            var config = GameConfig.FromDictionary(dictionary);

            Assert.Equal(gameName, config.Name);
            Assert.Equal(countryName, config.Countries.Single().Name);
            Assert.Equal(countrySide, config.Countries.Single().Side);
        }
    }
}
