namespace Md.Tga.SavePlayerMappingsSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ISavePlayerMappingsMessage, Function>
    {
        /// <summary>
        ///     Access the database.
        /// </summary>
        private readonly IPlayerMappingsDatabase database;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the database.</param>
        public FunctionProvider(ILogger<Function> logger, IPlayerMappingsDatabase database)
            : base(logger)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(ISavePlayerMappingsMessage message)
        {
            await this.database.InsertAsync(message.PlayerMappings);
        }
    }
}
