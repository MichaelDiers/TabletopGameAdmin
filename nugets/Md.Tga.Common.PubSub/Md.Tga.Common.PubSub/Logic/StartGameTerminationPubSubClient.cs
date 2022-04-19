namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="IStartGameTerminationMessage" />.
    /// </summary>
    public class StartGameTerminationPubSubClient
        : AbstractPubSubClient<IStartGameTerminationMessage>, IStartGameTerminationPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="StartGameTerminationPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public StartGameTerminationPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
