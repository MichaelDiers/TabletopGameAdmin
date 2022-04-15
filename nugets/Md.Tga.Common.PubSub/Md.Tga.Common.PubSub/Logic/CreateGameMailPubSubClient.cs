namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ICreateGameMailMessage" />.
    /// </summary>
    public class CreateGameMailPubSubClient : AbstractPubSubClient<ICreateGameMailMessage>, ICreateGameMailPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="CreateGameMailPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public CreateGameMailPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
