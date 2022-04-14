namespace Md.Tga.StartSurveySubscriber
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.GoogleCloudFunctions.Logic;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Microsoft.Extensions.Logging;
    using Surveys.Common.Contracts;
    using Surveys.Common.Messages;
    using Surveys.Common.Models;
    using Surveys.Common.PubSub.Contracts.Logic;
    using IPerson = Md.Tga.Common.Contracts.Models.IPerson;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : PubSubProvider<IStartSurveyMessage, Function>
    {
        /// <summary>
        ///     Access pub/sub client for saving surveys.
        /// </summary>
        private readonly ISaveSurveyPubSubClient pubSubClient;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="logger">An error logger.</param>
        /// <param name="pubSubClient">Access pub/sub client for saving surveys.</param>
        public FunctionProvider(ILogger<Function> logger, ISaveSurveyPubSubClient pubSubClient)
            : base(logger)
        {
            this.pubSubClient = pubSubClient;
        }

        /// <summary>
        ///     Handles the pub/sub messages.
        /// </summary>
        /// <param name="message">The message that is handled.</param>
        /// <returns>A <see cref="Task" />.</returns>
        protected override async Task HandleMessageAsync(IStartSurveyMessage message)
        {
            var survey = this.CreateSurvey(message);
            var saveSurveyMessage = new SaveSurveyMessage(message.ProcessId, survey);
            await this.pubSubClient.PublishAsync(saveSurveyMessage);
        }

        /// <summary>
        ///     Create the choices of the survey questions.
        /// </summary>
        /// <param name="countries">The countries of the game series.</param>
        /// <returns>An <see cref="IEnumerable{T}" /> of <see cref="IChoice" />.</returns>
        private IEnumerable<IChoice> CreateChoices(IEnumerable<ICountry> countries)
        {
            var order = 0;
            yield return new Choice(
                Guid.NewGuid().ToString(),
                Translations.ChoicePleaseChoose,
                false,
                order++);
            foreach (var country in countries)
            {
                yield return new Choice(
                    country.Id,
                    country.Name,
                    true,
                    order++);
            }
        }

        /// <summary>
        ///     Map <see cref="Person" /> to <see cref="Participant" />.
        /// </summary>
        /// <param name="players">The players of the game series.</param>
        /// <param name="questions">The questions of the survey.</param>
        /// <returns>An <see cref="IEnumerable{T}" /> of <see cref="Participant" />.</returns>
        private IEnumerable<IParticipant> CreateParticipants(
            IEnumerable<IPerson> players,
            IEnumerable<IQuestion> questions
        )
        {
            var questionReferences = questions.Select(
                    question => new QuestionReference(
                        question.Id,
                        question.Choices.First(choice => !choice.Selectable).Id))
                .ToArray();
            return players.OrderBy(player => player.Name)
                .Select(
                    (player, order) => new Participant(
                        player.Id,
                        player.Email,
                        player.Name,
                        questionReferences,
                        order));
        }

        /// <summary>
        ///     Create the questions of a survey.
        /// </summary>
        /// <param name="message">The incoming pub/sub message.</param>
        /// <returns>An <see cref="IEnumerable{T}" /> of <see cref="IQuestion" />.</returns>
        private IEnumerable<IQuestion> CreateQuestions(IStartSurveyMessage message)
        {
            var questions = new[] {Translations.Question1, Translations.Question2, Translations.Question3};

            return questions.Select(
                (text, order) => new Question(
                    Guid.NewGuid().ToString(),
                    text,
                    this.CreateChoices(message.GameSeries.Countries).ToArray(),
                    order));
        }

        /// <summary>
        ///     Create a new survey.
        /// </summary>
        /// <param name="message">The incoming pub/sub message.</param>
        /// <returns>An <see cref="ISurvey" />.</returns>
        private ISurvey CreateSurvey(IStartSurveyMessage message)
        {
            var questions = this.CreateQuestions(message).ToArray();
            var participants = this.CreateParticipants(message.GameSeries.Players, questions).ToArray();
            return new Survey(
                null,
                null,
                message.Game.DocumentId,
                message.Game.Name,
                "info",
                "link",
                new Person(
                    message.GameSeries.Organizer.Id,
                    message.GameSeries.Organizer.Email,
                    message.GameSeries.Organizer.Name),
                participants,
                questions);
        }
    }
}
