namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="IEvaluateGameTerminationMessage" />.
    /// </summary>
    public class EvaluateGameTerminationPubSubClient
        : AbstractPubSubClient<IEvaluateGameTerminationMessage>, IEvaluateGameTerminationPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="EvaluateGameTerminationPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public EvaluateGameTerminationPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
