namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="IStartSurveyMessage" />.
    /// </summary>
    public class StartSurveyPubSubClient : AbstractPubSubClient<IStartSurveyMessage>, IStartSurveyPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="StartSurveyPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public StartSurveyPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
