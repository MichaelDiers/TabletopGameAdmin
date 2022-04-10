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
            var mappings = new PlayerMappings(internalGameId, new[] {mapping});

            var fromDictionary = PlayerMappings.FromDictionary(mappings.ToDictionary());
            Assert.Equal(internalGameId, fromDictionary.InternalGameId);
            Assert.Equal(mapping.PlayerId, mappings.PlayerCountryMappings.First().PlayerId);
            Assert.Equal(mapping.CountryId, mappings.PlayerCountryMappings.First().CountryId);
        }
    }
}
