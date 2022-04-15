namespace Md.Tga.SurveyClosedSubscriber.Model
{
    using Md.GoogleCloudPubSub.Model;
    using Md.Tga.SurveyClosedSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : PubSubClientEnvironment, IFunctionConfiguration
    {
    }
}
