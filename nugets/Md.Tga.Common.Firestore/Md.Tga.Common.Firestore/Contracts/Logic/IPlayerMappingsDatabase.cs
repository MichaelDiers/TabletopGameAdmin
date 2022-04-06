namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Database operations of player-mappings collection.
    /// </summary>
    public interface IPlayerMappingsDatabase : IPlayerMappingsReadOnlyDatabase, IDatabase<IPlayerMappings>
    {
    }
}
