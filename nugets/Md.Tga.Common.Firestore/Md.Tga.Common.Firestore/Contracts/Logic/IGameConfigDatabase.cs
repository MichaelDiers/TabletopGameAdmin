namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Database operations of game config collection.
    /// </summary>
    public interface IGameConfigDatabase : IGameConfigReadOnlyDatabase, IDatabase<IGameConfig>
    {
    }
}
