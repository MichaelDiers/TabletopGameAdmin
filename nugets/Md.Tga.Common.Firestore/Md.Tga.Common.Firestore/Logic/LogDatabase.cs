namespace Md.Tga.Common.Firestore.Logic
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database for <see cref="Game" /> collections,
    /// </summary>
    public class LogDatabase : Database<ILogMessage>, ILogDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="LogDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public LogDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, LogReadOnlyDatabase.CollectionName, LogMessage.FromDictionary)
        {
        }
    }
}
