namespace Md.Tga.StartGameSubscriber.Logic
{
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.StartGameSubscriber.Contracts;

    /// <summary>
    ///     Client for accessing pub/sub.
    /// </summary>
    public class InitializeSurveyPubSubClient : PubSubClient, IInitializeSurveyPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="InitializeSurveyPubSubClient" />.
        /// </summary>
        /// <param name="configuration">The pub/sub configuration.</param>
        public InitializeSurveyPubSubClient(IPubSubClientConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
