namespace Md.Tga.StartGameSubscriber.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.StartGameSubscriber.Contracts;

    /// <summary>
    ///     Access to the database collection translations.
    /// </summary>
    public class TranslationsReadOnlyDatabase : ReadonlyDatabase, ITranslationsReadOnlyDatabase
    {
        /// <summary>
        ///     The german translations specifier.
        /// </summary>
        private const string GermanTranslations = "De";

        /// <summary>
        ///     The application configuration.
        /// </summary>
        private readonly IFunctionConfiguration configuration;

        /// <summary>
        ///     Creates a new instance of <see cref="GameSeriesReadOnlyDatabase" />.
        /// </summary>
        /// <param name="databaseConfiguration">The database configuration.</param>
        /// <param name="configuration">The application configuration.</param>
        public TranslationsReadOnlyDatabase(
            IDatabaseConfiguration databaseConfiguration,
            IFunctionConfiguration configuration
        )
            : base(databaseConfiguration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        ///     Read the german translations.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result contains the translations.</returns>
        public async Task<IDictionary<string, string>?> ReadGermanTranslations()
        {
            var result = await this.ReadByDocumentIdAsync(this.configuration.TranslationsDocument);
            if (result != null && result.ContainsKey(GermanTranslations))
            {
                var translations = result[GermanTranslations] as IDictionary<string, object>;
                if (translations != null)
                {
                    return new Dictionary<string, string>(
                        translations.Select(pair => new KeyValuePair<string, string>(pair.Key, (string)pair.Value)));
                }
            }

            return null;
        }
    }
}
