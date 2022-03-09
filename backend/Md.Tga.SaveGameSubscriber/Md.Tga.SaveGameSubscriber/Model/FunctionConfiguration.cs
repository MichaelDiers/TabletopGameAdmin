namespace Md.Tga.SaveGameSubscriber.Model
{
    using Md.Tga.SaveGameSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : IFunctionConfiguration
    {
        /// <summary>
        ///     Gets or sets the name of the database collection.
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        ///     Gets or sets the project id.
        /// </summary>
        public string ProjectId { get; set; }
    }
}
