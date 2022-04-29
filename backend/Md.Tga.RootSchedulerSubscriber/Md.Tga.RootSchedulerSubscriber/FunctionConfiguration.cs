namespace Md.Tga.RootSchedulerSubscriber
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : IRuntimeEnvironment
    {
        /// <summary>
        ///     Gets or sets the topic names for sending scheduler messages.
        /// </summary>
        public IEnumerable<string> TopicNames { get; set; }

        /// <summary>
        ///     Gets or sets the runtime environment.
        /// </summary>
        public Environment Environment { get; set; }

        /// <summary>
        ///     Gets or sets the id of the project.
        /// </summary>
        public string ProjectId { get; set; }
    }
}
