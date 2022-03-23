namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Models;

    /// <summary>
    ///     Database operations of games collection.
    /// </summary>
    public interface IGameDatabase : IGameReadOnlyDatabase, IDatabase<Game>
    {
    }
}
