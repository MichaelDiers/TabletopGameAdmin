namespace Md.Tga.Common.PubSub.Contracts.Logic
{
    using System.Threading.Tasks;
    using Md.Tga.Common.Contracts.Messages;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveGameTerminationSurveyResultMessage" />.
    /// </summary>
    public interface ISaveGameTerminationSurveyResultPubSubClient
    {
        /// <summary>
        ///     Publish a <see cref="ISaveGameTerminationSurveyResultMessage" /> message.
        /// </summary>
        /// <param name="message">The message to publish.</param>
        /// <returns>A <see cref="Task" /> that indicates completion.</returns>
        // ReSharper disable once UnusedMember.Global
        Task PublishAsync(ISaveGameTerminationSurveyResultMessage message);
    }
}
