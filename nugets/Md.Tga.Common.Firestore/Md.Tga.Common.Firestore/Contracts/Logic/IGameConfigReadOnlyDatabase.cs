namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Database operations of game configs collection.
    /// </summary>
    public interface IGameConfigReadOnlyDatabase : IReadOnlyDatabase<IGameConfig>
    {
    }
}
