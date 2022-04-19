namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveGameTerminationSurveyMessage" />.
    /// </summary>
    public class SaveGameTerminationSurveyPubSubClient
        : AbstractPubSubClient<ISaveGameTerminationSurveyMessage>, ISaveGameTerminationSurveyPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameTerminationSurveyPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SaveGameTerminationSurveyPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
