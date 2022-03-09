namespace Md.Tga.StartGameSubscriber.Logic
{
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.StartGameSubscriber.Contracts;

    /// <summary>
    ///     Client for accessing pub/sub.
    /// </summary>
    public class SaveGamePubSubClient : PubSubClient, ISaveGamePubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGamePubSubClient" />.
        /// </summary>
        /// <param name="configuration">The pub/sub configuration.</param>
        public SaveGamePubSubClient(IPubSubClientConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
