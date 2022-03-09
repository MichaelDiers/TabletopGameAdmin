namespace Md.Tga.StartGameSubscriber.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Access to the database collection translations.
    /// </summary>
    public interface ITranslationsReadOnlyDatabase : IReadOnlyDatabase
    {
        /// <summary>
        ///     Read the german translations.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result contains the translations.</returns>
        Task<IDictionary<string, string>?> ReadGermanTranslations();
    }
}
