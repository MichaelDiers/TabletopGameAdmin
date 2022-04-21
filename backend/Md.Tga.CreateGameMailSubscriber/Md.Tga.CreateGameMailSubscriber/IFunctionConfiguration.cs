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
        ///     Gets the format string for the terminate link.
        /// </summary>
        string TerminateLinkFormat { get; }
    }
}
