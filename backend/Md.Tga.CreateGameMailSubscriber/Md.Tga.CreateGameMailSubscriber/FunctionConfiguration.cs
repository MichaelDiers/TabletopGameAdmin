namespace Md.Tga.CreateGameMailSubscriber
{
    using Md.GoogleCloudPubSub.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : PubSubClientEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the format string for the terminate link.
        /// </summary>
        public string TerminateLinkFormat { get; set; }
    }
}
