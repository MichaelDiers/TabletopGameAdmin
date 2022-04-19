namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveGameTerminationSurveyResultMessage" />.
    /// </summary>
    public class SaveGameTerminationSurveyResultPubSubClient
        : AbstractPubSubClient<ISaveGameTerminationSurveyResultMessage>, ISaveGameTerminationSurveyResultPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameTerminationSurveyResultPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SaveGameTerminationSurveyResultPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
