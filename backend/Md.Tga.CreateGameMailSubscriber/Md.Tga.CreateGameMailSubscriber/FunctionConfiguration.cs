namespace Md.Tga.CreateGameMailSubscriber
{
    using Md.GoogleCloudPubSub.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : PubSubClientEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets or sets the name for the start game attachment.
        /// </summary>
        public string StartGameAttachmentName { get; set; }

        /// <summary>
        ///     Gets or sets the url for the start game attachment.
        /// </summary>
        public string StartGameAttachmentUrl { get; set; }

        /// <summary>
        ///     Gets the format string for the terminate link.
        /// </summary>
        public string TerminateLinkFormat { get; set; }
    }
}
