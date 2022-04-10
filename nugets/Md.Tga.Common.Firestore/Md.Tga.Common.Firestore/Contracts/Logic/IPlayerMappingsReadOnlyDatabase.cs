namespace Md.Tga.Common.Firestore.Contracts.Logic
{
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.Tga.Common.Contracts.Models;

    /// <summary>
    ///     Database operations on the player-mappings collection.
    /// </summary>
    public interface IPlayerMappingsReadOnlyDatabase : IReadOnlyDatabase<IPlayerMappings>
    {
    }
}
