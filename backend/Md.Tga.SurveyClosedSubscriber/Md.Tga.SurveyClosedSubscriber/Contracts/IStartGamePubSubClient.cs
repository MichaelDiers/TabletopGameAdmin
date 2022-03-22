namespace Md.Tga.InitializeGameSeriesSubscriber.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Client for publishing messages to start a new game.
    /// </summary>
    public interface IStartGamePubSubClient : IPubSubClient
    {
    }
}
