namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveGameMessage" />.
    /// </summary>
    public class SaveGamePubSubClient : AbstractPubSubClient<ISaveGameMessage>, ISaveGamePubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGamePubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SaveGamePubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
