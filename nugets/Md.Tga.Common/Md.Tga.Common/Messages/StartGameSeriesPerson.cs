namespace Md.Tga.Common.Messages
{
    using Md.Common.Extensions;
    using Md.Tga.Common.Contracts.Messages;
    using Newtonsoft.Json;

    /// <summary>
    ///     Describes a player or organizer of the game series.
    /// </summary>
    public class StartGameSeriesPerson : IStartGameSeriesPerson
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="StartGameSeriesPerson" />.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        /// <param name="email">The email of the person.</param>
        public StartGameSeriesPerson(string name, string email)
        {
            this.Name = name.ValidateIsNotNullOrWhitespace(nameof(name));
            this.Email = email.ValidateIsAnEmail(nameof(email));
        }

        /// <summary>
        ///     Gets the email of the person.
        /// </summary>
        [JsonProperty("email", Required = Required.Always, Order = 2)]
        public string Email { get; }

        /// <summary>
        ///     Gets the name of the person.
        /// </summary>
        [JsonProperty("name", Required = Required.Always, Order = 1)]
        public string Name { get; }
    }
}
