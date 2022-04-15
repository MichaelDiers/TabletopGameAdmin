namespace Md.Tga.CreateGameMailSubscriber
{
    using Md.GoogleCloudPubSub.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : PubSubClientEnvironment, IFunctionConfiguration
    {
    }
}
