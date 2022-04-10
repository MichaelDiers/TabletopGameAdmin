namespace Md.Tga.Common.Tests.Models
{
    using System;
    using System.Linq;
    using Md.Tga.Common.Models;
    using Xunit;

    public class PlayerMappingsTest
    {
        [Fact]
        public void FromDictionary()
        {
            var internalGameId = Guid.NewGuid().ToString();
            var mapping = new PlayerCountryMapping(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            var mappings = new PlayerMappings(
                internalGameId,
                DateTime.Now,
                Guid.NewGuid().ToString(),
                new[] {mapping});

            var fromDictionary = PlayerMappings.FromDictionary(mappings.ToDictionary());
            Assert.Equal(mapping.PlayerId, fromDictionary.PlayerCountryMappings.First().PlayerId);
            Assert.Equal(mapping.CountryId, fromDictionary.PlayerCountryMappings.First().CountryId);
        }
    }
}
