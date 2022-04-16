namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ILogMessage" />.
    /// </summary>
    public class LogPubSubClient : AbstractPubSubClient<ILogMessage>, ILogPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="LogPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public LogPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
