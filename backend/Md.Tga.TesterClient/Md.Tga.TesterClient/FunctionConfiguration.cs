namespace Md.Tga.TesterClient
{
    using System.ComponentModel.DataAnnotations;
    using Md.Common.DataAnnotations;
    using Md.Common.Model;

    /// <summary>
    ///     Describes the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment
    {
        /// <summary>
        ///     Gets or sets the name of the pub/sub topic for saving surveys.
        /// </summary>
        [Required]
        [TopicName]
        public string SaveSurveyResultTopicName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the name of the pub/sub topic for starting a new game series.
        /// </summary>
        [Required]
        [TopicName]
        public string StartGameSeriesTopicName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the name of the pub/sub topic for starting the game termination process.
        /// </summary>
        [Required]
        [TopicName]
        public string StartGameTerminationTopicName { get; set; } = string.Empty;
    }
}
