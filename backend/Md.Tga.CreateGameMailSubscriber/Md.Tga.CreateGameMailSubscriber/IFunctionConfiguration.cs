namespace Md.Tga.CreateGameMailSubscriber
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudPubSub.Contracts.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public interface IFunctionConfiguration : IRuntimeEnvironment, IPubSubClientEnvironment
    {
        /// <summary>
        ///     Gets the name for the start game attachment.
        /// </summary>
        string StartGameAttachmentName { get; }

        /// <summary>
        ///     Gets the url for the start game attachment.
        /// </summary>
        string StartGameAttachmentUrl { get; }

        /// <summary>
        ///     Gets the format of the statistics link.
        /// </summary>
        string StatisticsLinkFormat { get; }

        /// <summary>
        ///     Gets the format string for the terminate link.
        /// </summary>
        string TerminateLinkFormat { get; }
    }
}
