namespace Md.Tga.StartGameSeriesSubscriber
{
    using Md.Common.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets or sets the pub/sub topic name.
        /// </summary>
        public string TopicName { get; set; }
    }
}
