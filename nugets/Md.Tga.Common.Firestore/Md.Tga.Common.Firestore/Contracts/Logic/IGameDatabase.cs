namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Database operations of games collection.
    /// </summary>
    public interface IGameDatabase : IGameReadOnlyDatabase, IDatabase<IGame>
    {
    }
}
