namespace Md.Tga.InitializeGameSeriesSubscriber.Logic
{
    using Md.GoogleCloud.Base.Logic;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.InitializeGameSeriesSubscriber.Contracts;

    /// <summary>
    ///     Client for publishing messages to start a new game.
    /// </summary>
    public class StartGamePubSubClient : PubSubClient, IStartGamePubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="StartGamePubSubClient" />.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public StartGamePubSubClient(IFunctionConfiguration configuration)
            : base(new PubSubClientConfiguration(configuration.ProjectId, configuration.StartGameTopicName))
        {
        }
    }
}
