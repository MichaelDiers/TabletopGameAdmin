namespace TabletopGameAdmin.Common.Contracts.Models
{
    /// <summary>
    ///     Describes a person.
    /// </summary>
    public interface IPerson : INamedBase
    {
        /// <summary>
        ///     Gets the email of the person.
        /// </summary>
        string Email { get; }
    }
}