namespace Md.Tga.InitializeGameSeriesSubscriber.Logic
{
    using Md.GoogleCloud.Base.Logic;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.InitializeGameSeriesSubscriber.Contracts;

    /// <summary>
    ///     Client for publishing messages to save game series data.
    /// </summary>
    public class SaveGameSeriesPubSubClient : PubSubClient, ISaveGameSeriesPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameSeriesPubSubClient" />.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public SaveGameSeriesPubSubClient(IFunctionConfiguration configuration)
            : base(new PubSubClientConfiguration(configuration.ProjectId, configuration.SaveGameSeriesTopicName))
        {
        }
    }
}
