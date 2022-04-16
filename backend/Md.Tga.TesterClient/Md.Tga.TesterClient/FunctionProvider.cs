namespace Md.Tga.TesterClient
{
    using System;
    using System.Threading.Tasks;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : IFunctionProvider
    {
        /// <summary>
        ///     Client for sending a message to pub/sub.
        /// </summary>
        private readonly IStartGameSeriesPubSubClient pubSubClient;

        /// <summary>
        ///     Access test data.
        /// </summary>
        private readonly ITestDataReadOnlyDatabase testDataReadOnlyDatabase;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="pubSubClient">Client for sending a message to pub/sub.</param>
        /// <param name="testDataReadOnlyDatabase">Access test data.</param>
        public FunctionProvider(
            IStartGameSeriesPubSubClient pubSubClient,
            ITestDataReadOnlyDatabase testDataReadOnlyDatabase
        )
        {
            this.pubSubClient = pubSubClient ?? throw new ArgumentNullException(nameof(pubSubClient));
            this.testDataReadOnlyDatabase = testDataReadOnlyDatabase ??
                                            throw new ArgumentNullException(nameof(testDataReadOnlyDatabase));
        }

        /// <summary>
        ///     Read the test case from the database and publish the message to pub/sub to start the process.
        /// </summary>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task InitializeGameSeries()
        {
            var startGameSeriesMessage = await this.testDataReadOnlyDatabase.ReadStartGameSeriesMessageAsync();
            await this.pubSubClient.PublishAsync(startGameSeriesMessage);
        }
    }
}
