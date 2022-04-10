namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISavePlayerMappingsMessage" />.
    /// </summary>
    public class SavePlayerMappingsPubSubClient
        : AbstractPubSubClient<ISavePlayerMappingsMessage>, ISavePlayerMappingsPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SavePlayerMappingsPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SavePlayerMappingsPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
