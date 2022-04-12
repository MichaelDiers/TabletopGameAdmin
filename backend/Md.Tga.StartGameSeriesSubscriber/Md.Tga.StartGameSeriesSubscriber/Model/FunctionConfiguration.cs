namespace Md.Tga.StartGameSeriesSubscriber.Model
{
    using Md.Common.Model;
    using Md.Tga.StartGameSeriesSubscriber.Contracts;

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
