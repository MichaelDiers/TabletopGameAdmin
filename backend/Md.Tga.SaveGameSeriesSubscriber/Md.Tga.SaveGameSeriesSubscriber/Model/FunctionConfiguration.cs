namespace Md.Tga.SaveGameSeriesSubscriber.Model
{
    using Md.Common.Model;
    using Md.Tga.SaveGameSeriesSubscriber.Contracts;

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
