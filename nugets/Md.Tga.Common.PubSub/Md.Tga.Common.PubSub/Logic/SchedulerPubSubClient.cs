namespace Md.Tga.Common.PubSub.Logic
{
    using Md.Common.Contracts.Messages;
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="IMessage" />.
    /// </summary>
    public class SchedulerPubSubClient : AbstractPubSubClient<IMessage>, ISchedulerPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SchedulerPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SchedulerPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
