namespace Md.Tga.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="IStartGameSeriesMessage" />.
    /// </summary>
    public class StartGameSeriesPubSubClient
        : AbstractPubSubClient<IStartGameSeriesMessage>, IStartGameSeriesPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="StartGameSeriesPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public StartGameSeriesPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
