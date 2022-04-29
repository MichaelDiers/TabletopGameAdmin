namespace Md.Tga.Common.PubSub.Contracts.Logic
{
    using System.Threading.Tasks;
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     A client for publishing messages to a scheduler.
    /// </summary>
    public interface ISchedulerPubSubClient
    {
        /// <summary>
        ///     Publish a <see cref="IMessage" /> message.
        /// </summary>
        /// <param name="message">The message to publish.</param>
        /// <returns>A <see cref="Task" /> that indicates completion.</returns>
        Task PublishAsync(IMessage message);
    }
}
