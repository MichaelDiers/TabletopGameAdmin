namespace Md.Tga.Common.Contracts.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    ///     Describes the status of a game.
    /// </summary>
    public enum Status
    {
        /// <summary>
        ///     Undefined value.
        /// </summary>
        None = 0,

        [EnumMember(Value = "CLOSED")]
        Closed = 1
    }
}
