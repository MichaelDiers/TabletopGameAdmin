namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;

    /// <summary>
    ///     ReadOnly database for <see cref="LogMessage" /> collections,
    /// </summary>
    public class LogReadOnlyDatabase : ReadonlyDatabase<ILogMessage>, ILogReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the database collection.
        /// </summary>
        public const string CollectionName = "logs";

        /// <summary>
        ///     Creates a new instance of <see cref="LogReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public LogReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, LogReadOnlyDatabase.CollectionName, LogMessage.FromDictionary)
        {
        }
    }
}
