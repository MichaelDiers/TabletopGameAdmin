namespace Md.Tga.Common.PubSub.Contracts.Logic
{
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Messages;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveGameSeriesMessage" />.
    /// </summary>
    public interface ISaveGameSeriesPubSubClient
    {
        /// <summary>
        ///     Publish a <see cref="ISaveGameSeriesMessage" /> message.
        /// </summary>
        /// <param name="message">The message to publish.</param>
        /// <returns>A <see cref="Task" /> that indicates completion.</returns>
        // ReSharper disable once UnusedMember.Global
        Task PublishAsync(ISaveGameSeriesMessage message);
    }
}
