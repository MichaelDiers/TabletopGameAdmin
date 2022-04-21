namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveGameStatusMessage" />.
    /// </summary>
    public class SaveGameStatusPubSubClient : AbstractPubSubClient<ISaveGameStatusMessage>, ISaveGameStatusPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameStatusPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SaveGameStatusPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
