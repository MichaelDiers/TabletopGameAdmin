﻿namespace Md.Tga.SaveGameSeriesSubscriber.Logic
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
    public class FunctionProvider : PubSubProvider<ISaveGameSeriesMessage, Function>
    {
        /// <summary>
        ///     Access the database.
        /// </summary>
        private readonly IGameSeriesDatabase database;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="database">Access to the database.</param>
        public FunctionProvider(ILogger<Function> logger, IGameSeriesDatabase database)
            : base(logger)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(ISaveGameSeriesMessage message)
        {
            await this.database.InsertAsync(message.InternalId, message.GameSeries);
        }
    }
}
