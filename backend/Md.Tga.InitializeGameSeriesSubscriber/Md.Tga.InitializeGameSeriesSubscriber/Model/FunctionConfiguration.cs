namespace Md.Tga.InitializeGameSeriesSubscriber.Model
{
    using Md.Tga.InitializeGameSeriesSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : IFunctionConfiguration
    {
        /// <summary>
        ///     Gets or sets the project id.
        /// </summary>
        public string ProjectId { get; set; }
    }
}
