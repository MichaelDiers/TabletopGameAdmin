namespace Md.Tga.Common.Models.MultiLanguage
{
    using System.Collections.Generic;
    using Md.Common.Extensions;
    using Md.Common.Model;
    using Md.Tga.Common.Contracts.Models.MultiLanguage;

    /// <summary>
    ///     Describes available translations.
    /// </summary>
    public class Translations : ToDictionaryConverter, ITranslations
    {
        /// <summary>
        ///     The database name for german translations.
        /// </summary>
        private const string GermanName = "De";

        /// <summary>
        ///     Creates a new instance of <see cref="Translations" />.
        /// </summary>
        /// <param name="german"></param>
        public Translations(ITranslation german)
        {
            this.German = german;
        }

        /// <summary>
        ///     Add object values to dictionary.
        /// </summary>
        /// <param name="dictionary">The data is added to this dictionary.</param>
        /// <returns>The dictionary given as parameter.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            dictionary.Add(Translations.GermanName, this.German.ToDictionary());
            return dictionary;
        }

        /// <summary>
        ///     Gets the german translations.
        /// </summary>
        public ITranslation German { get; }

        /// <summary>
        ///     Create an <see cref="ITranslations" /> from database data.
        /// </summary>
        /// <param name="dictionary">The data from the database.</param>
        /// <returns>An <see cref="ITranslations" />.</returns>
        public static ITranslations FromDictionary(IDictionary<string, object> dictionary)
        {
            return new Translations(Translation.FromDictionary(dictionary.GetDictionary(Translations.GermanName)));
        }
    }
}
