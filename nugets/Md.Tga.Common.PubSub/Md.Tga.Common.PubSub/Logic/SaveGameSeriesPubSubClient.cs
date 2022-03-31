namespace Md.Tga.Common.PubSub.Logic
{
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveGameSeriesMessage" />.
    /// </summary>
    public class SaveGameSeriesPubSubClient : AbstractPubSubClient<ISaveGameSeriesMessage>, ISaveGameSeriesPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SaveGameSeriesPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SaveGameSeriesPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
