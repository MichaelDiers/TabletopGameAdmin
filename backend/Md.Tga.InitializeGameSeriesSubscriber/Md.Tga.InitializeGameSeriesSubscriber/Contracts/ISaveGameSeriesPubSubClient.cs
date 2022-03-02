namespace Md.Tga.InitializeGameSeriesSubscriber.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Client for publishing messages to save game series data.
    /// </summary>
    public interface ISaveGameSeriesPubSubClient : IPubSubClient
    {
    }
}
