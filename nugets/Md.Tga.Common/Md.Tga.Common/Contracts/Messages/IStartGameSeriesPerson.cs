namespace Md.Tga.Common.Contracts.Messages
{
    /// <summary>
    ///     Describes a player or organizer of the game series.
    /// </summary>
    public interface IStartGameSeriesPerson
    {
        /// <summary>
        ///     Gets the email of the person.
        /// </summary>
        string Email { get; }

        /// <summary>
        ///     Gets the name of the person.
        /// </summary>
        string Name { get; }
    }
}
