namespace Md.Tga.SavePlayerMappingsSubscriber
{
    using Md.GoogleCloudPubSub.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : PubSubClientEnvironment, IFunctionConfiguration
    {
    }
}
