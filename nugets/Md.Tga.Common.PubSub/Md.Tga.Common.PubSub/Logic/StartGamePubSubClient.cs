namespace Md.Tga.Common.PubSub.Logic
{
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="IStartGameMessage" />.
    /// </summary>
    public class StartGamePubSubClient : AbstractPubSubClient<IStartGameMessage>, IStartGamePubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="StartGamePubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public StartGamePubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
