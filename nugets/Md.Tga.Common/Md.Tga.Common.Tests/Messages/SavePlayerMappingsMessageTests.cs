namespace Md.Tga.Common.Tests.Messages
{
    using System;
    using System.Linq;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Xunit;

    public class SavePlayerMappingsMessageTests
    {
        [Fact]
        public void Ctor()
        {
            var processId = Guid.NewGuid().ToString();
            var playerMapping = new PlayerCountryMapping(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            var message = new SavePlayerMappingsMessage(
                processId,
                new PlayerMappings(
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
                    Guid.NewGuid().ToString(),
                    new[] {playerMapping}));

            Assert.Equal(processId, message.ProcessId);
            Assert.Equal(message.PlayerMappings.PlayerCountryMappings.Single().PlayerId, playerMapping.PlayerId);
            Assert.Equal(message.PlayerMappings.PlayerCountryMappings.Single().CountryId, playerMapping.CountryId);

            var iMessage = (ISavePlayerMappingsMessage) message;
            Assert.Equal(processId, iMessage.ProcessId);
            Assert.Equal(iMessage.PlayerMappings.PlayerCountryMappings.Single().PlayerId, playerMapping.PlayerId);
            Assert.Equal(iMessage.PlayerMappings.PlayerCountryMappings.Single().CountryId, playerMapping.CountryId);
        }
    }
}
