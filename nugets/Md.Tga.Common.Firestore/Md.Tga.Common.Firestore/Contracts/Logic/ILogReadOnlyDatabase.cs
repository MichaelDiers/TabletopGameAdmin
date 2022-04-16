namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Messages;

    /// <summary>
    ///     Database operations of log collection.
    /// </summary>
    public interface ILogReadOnlyDatabase : IReadOnlyDatabase<ILogMessage>
    {
    }
}
