namespace Md.Tga.SurveyClosedSubscriber.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Md.Common.Database;
    using Md.GoogleCloudFunctions.Logic;
    using Md.Tga.Common.Contracts.Models;
    using Md.Tga.Common.Firestore.Contracts.Logic;
    using Md.Tga.Common.Messages;
    using Md.Tga.Common.Models;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Md.Tga.SurveyClosedSubscriber.Contracts;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<ISurveyClosedMessage, Function>
    {
        private readonly IGameReadOnlyDatabase gamesDatabase;
        private readonly IGameSeriesReadOnlyDatabase gameSeriesDatabase;
        private readonly ISavePlayerMappingsPubSubClient savePlayerMappingsPubSubClient;
        private readonly ISendMailPubSubClient sendMailPubSubClient;
        private readonly ISurveyEvaluator surveyEvaluator;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="gamesDatabase"></param>
        /// <param name="gameSeriesDatabase"></param>
        /// <param name="surveyEvaluator"></param>
        /// <param name="savePlayerMappingsPubSubClient">Client for publishing save player mappings.</param>
        /// <param name="sendMailPubSubClient">Client publishing send mail messages.</param>
        public FunctionProvider(
            ILogger<Function> logger,
            IGameSeriesReadOnlyDatabase gameSeriesDatabase,
            IGameReadOnlyDatabase gamesDatabase,
            ISurveyEvaluator surveyEvaluator,
            ISavePlayerMappingsPubSubClient savePlayerMappingsPubSubClient,
            ISendMailPubSubClient sendMailPubSubClient
        )
            : base(logger)
        {
            this.gameSeriesDatabase = gameSeriesDatabase;
            this.gamesDatabase = gamesDatabase;

            this.surveyEvaluator = surveyEvaluator;
            this.savePlayerMappingsPubSubClient = savePlayerMappingsPubSubClient;
            this.sendMailPubSubClient = sendMailPubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(ISurveyClosedMessage message)
        {
            var game = await this.gamesDatabase.ReadOneAsync(
                DatabaseObject.DocumentIdName,
                message.Survey.ParentDocumentId);
            if (game == null)
            {
                throw new ArgumentException($"No game found for survey id {message.Survey.ParentDocumentId}");
            }

            var gameSeries = await this.gameSeriesDatabase.ReadByDocumentIdAsync(game.ParentDocumentId);

            var solution = this.surveyEvaluator.Evaluate(gameSeries, message.Results).ToArray();

            await this.savePlayerMappingsPubSubClient.PublishAsync(
                new SavePlayerMappingsMessage(
                    message.ProcessId,
                    gameSeries,
                    game,
                    new PlayerMappings(
                        null,
                        null,
                        game.DocumentId,
                        solution)));
        }

        private Body CreateBody(
            IGameSeries gameSeries,
            IPerson person,
            XElement htmlSolution,
            string plainSolution
        )
        {
            var htmlTemplate = File.ReadAllText("./Data/template.html");
            var textTemplate = File.ReadAllText("./Data/template.txt");

            return new Body(
                string.Format(
                    htmlTemplate,
                    person.Name,
                    htmlSolution,
                    gameSeries.Organizer.Name),
                string.Format(
                    textTemplate,
                    person.Name,
                    plainSolution,
                    gameSeries.Organizer.Name));
        }

        private (XElement html, string plain) CreateSolutionForBody(
            IGameSeries gameSeries,
            IEnumerable<IPlayerCountryMapping> playerCountryMappings
        )
        {
            var mapping = playerCountryMappings.ToArray();

            var builder = new StringBuilder();

            var html = new XElement("ul");

            foreach (var gameSeriesCountry in gameSeries.Countries)
            {
                var countryName = gameSeriesCountry.Name;
                var playerId = mapping.First(m => m.CountryId == gameSeriesCountry.Id).PlayerId;
                var playerName = gameSeries.Players.First(p => p.Id == playerId).Name;
                html.Add(new XElement("li", $"{countryName}: {playerName}"));
                builder.AppendLine($"\t- {countryName}: {playerName}");
            }

            return (html, builder.ToString());
        }

        private async Task SendMail(
            ISurveyClosedMessage message,
            IGameSeries gameSeries,
            IGame game,
            IEnumerable<IPlayerCountryMapping> playerCountryMappings
        )
        {
            var (html, plain) = this.CreateSolutionForBody(gameSeries, playerCountryMappings);

            foreach (var gameSeriesPlayer in gameSeries.Players)
            {
                var sendMailMessage = new SendMailMessage(
                    message.ProcessId,
                    new[] {new Recipient(gameSeriesPlayer.Email, gameSeriesPlayer.Name)},
                    new Recipient(gameSeries.Organizer.Email, gameSeries.Organizer.Name),
                    $"Spiel {game.Name} kann starten!",
                    this.CreateBody(
                        gameSeries,
                        gameSeriesPlayer,
                        html,
                        plain));
                await this.sendMailPubSubClient.PublishAsync(sendMailMessage);
            }
        }
    }
}
