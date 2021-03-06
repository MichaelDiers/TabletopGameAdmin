namespace Md.Tga.Common.Firestore.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Md.Common.Contracts.Model;
    using Md.Common.Extensions;
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Md.GoogleCloudFirestore.Model;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Newtonsoft.Json;

    /// <summary>
    ///     Read test data from gameSeriesDatabase.
    /// </summary>
    public class TestDataReadOnlyDatabase : ITestDataReadOnlyDatabase
    {
        /// <summary>
        ///     Name of the test data collection.
        /// </summary>
        private const string CollectionName = "test-data";

        /// <summary>
        ///     Id of the game series document.
        /// </summary>
        private const string StartGameSeriesMessageDocumentId = "StartGameSeriesMessage";

        /// <summary>
        ///     The id of the json field containing game series data.
        /// </summary>
        private const string StartGameSeriesMessageJsonId = "json";

        /// <summary>
        ///     Access the test-data database.
        /// </summary>
        private readonly IDatabase<IDictionary<string, object>> database;

        /// <summary>
        ///     Creates a new instance of <see cref="TestDataReadOnlyDatabase" />.
        /// </summary>
        /// <param name="environment">The runtime environment.</param>
        public TestDataReadOnlyDatabase(IRuntimeEnvironment environment)
        {
            this.database = new Database<IDictionary<string, object>>(
                new DatabaseConfiguration(environment.ProjectId, TestDataReadOnlyDatabase.CollectionName),
                x => x);
        }

        /// <summary>
        ///     Read test data for game series.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result is a <see cref="IStartGameSeriesMessage" />.</returns>
        public async Task<IStartGameSeriesMessage> ReadStartGameSeriesMessageAsync()
        {
            var data = JsonConvert.DeserializeObject<StartGameSeriesMessage>(
                (await this.database.ReadByDocumentIdAsync(TestDataReadOnlyDatabase.StartGameSeriesMessageDocumentId))
                ?.GetString(TestDataReadOnlyDatabase.StartGameSeriesMessageJsonId) ??
                string.Empty);
            if (data == null)
            {
                throw new Exception(
                    $"Cannot read start game series message data for document id {TestDataReadOnlyDatabase.StartGameSeriesMessageDocumentId}");
            }

            return data;
        }
    }
}
