namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveGameTerminationResultMessage" />.
    /// </summary>
    public class SaveGameTerminationResultPubSubClient
        : AbstractPubSubClient<ISaveGameTerminationResultMessage>, ISaveGameTerminationResultPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameTerminationResultPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SaveGameTerminationResultPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
