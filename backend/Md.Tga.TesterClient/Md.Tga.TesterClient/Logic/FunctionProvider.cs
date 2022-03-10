namespace Md.Tga.TesterClient.Logic
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.TesterClient.Contracts;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : IFunctionProvider
    {
        /// <summary>
        ///     Access the application configuration.
        /// </summary>
        private readonly IFunctionConfiguration configuration;

        /// <summary>
        ///     Access to the database.
        /// </summary>
        private readonly IReadOnlyDatabase database;

        /// <summary>
        ///     Client for sending a message to pub/sub.
        /// </summary>
        private readonly IPubSubClient pubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="database">Access to the database.</param>
        /// <param name="pubSubClient">Client for sending a message to pub/sub.</param>
        /// <param name="configuration">The application configuration.</param>
        public FunctionProvider(
            IReadOnlyDatabase database,
            IPubSubClient pubSubClient,
            IFunctionConfiguration configuration
        )
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.pubSubClient = pubSubClient ?? throw new ArgumentNullException(nameof(pubSubClient));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        ///     Read the test case from the database and publish the message to pub/sub to start the process.
        /// </summary>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task InitializeGameSeries()
        {
            var dictionary = await this.database.ReadByDocumentIdAsync(this.configuration.DocumentId);
            if (dictionary != null)
            {
                var gameSeries = GameSeries.FromDictionary(dictionary);
                var message = new InitializeGameSeriesMessage(Guid.NewGuid().ToString(), gameSeries);
                await this.pubSubClient.PublishAsync(message);
            }
            else
            {
                throw new InvalidOperationException($"Cannot find test data: {this.configuration.DocumentId}");
            }
        }
    }
}
