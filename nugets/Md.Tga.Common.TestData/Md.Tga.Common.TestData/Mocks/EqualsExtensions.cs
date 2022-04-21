namespace Md.Tga.Common.TestData.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Contracts.Database;
    using Md.Tga.Common.Contracts.Messages;
    using Md.Tga.Common.Contracts.Models;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;
    using IPerson = Surveys.Common.Contracts.IPerson;
    using Status = Surveys.Common.Contracts.Status;

    public static class EqualsExtensions
    {
        public static bool CheckEqual(this ISurveyClosedMessage? expected, ISurveyClosedMessage? actual)
        {
            if (expected == null && actual == null)
            {
                return true;
            }

            if (expected == null || actual == null)
            {
                return false;
            }

            return expected.Survey.CheckEqual(actual.Survey) &&
                   expected.Results.CheckEqual(actual.Results, false) &&
                   expected.ProcessId.CheckEqual(actual.ProcessId);
        }

        public static bool CheckEqual(
            this IEnumerable<ISurveyResult> expected,
            IEnumerable<ISurveyResult> actual,
            bool ordered
        )
        {
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            if (expectedArray.Length != actualArray.Length)
            {
                return false;
            }

            if (ordered)
            {
                return expectedArray.Zip(actualArray).All(results => results.First.CheckEqual(results.Second));
            }

            return expectedArray.All(expectedResult => actualArray.Any(expectedResult.CheckEqual));
        }

        public static bool CheckEqual(
            this IEnumerable<IQuestionReference> expected,
            IEnumerable<IQuestionReference> actual,
            bool ordered
        )
        {
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            if (expectedArray.Length != actualArray.Length)
            {
                return false;
            }

            if (ordered)
            {
                return expectedArray.Zip(actualArray).All(results => results.First.CheckEqual(results.Second));
            }

            return expectedArray.All(expectedResult => actualArray.Any(expectedResult.CheckEqual));
        }

        public static bool CheckEqual(this IEnumerable<IQuestion> expected, IEnumerable<IQuestion> actual, bool ordered)
        {
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            if (expectedArray.Length != actualArray.Length)
            {
                return false;
            }

            if (ordered)
            {
                return expectedArray.Zip(actualArray).All(results => results.First.CheckEqual(results.Second));
            }

            return expectedArray.All(expectedResult => actualArray.Any(expectedResult.CheckEqual));
        }

        public static bool CheckEqual(
            this IEnumerable<IParticipant> expected,
            IEnumerable<IParticipant> actual,
            bool ordered
        )
        {
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            if (expectedArray.Length != actualArray.Length)
            {
                return false;
            }

            if (ordered)
            {
                return expectedArray.Zip(actualArray).All(results => results.First.CheckEqual(results.Second));
            }

            return expectedArray.All(expectedResult => actualArray.Any(expectedResult.CheckEqual));
        }

        public static bool CheckEqual(this IEnumerable<IChoice> expected, IEnumerable<IChoice> actual, bool ordered)
        {
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            if (expectedArray.Length != actualArray.Length)
            {
                return false;
            }

            if (ordered)
            {
                return expectedArray.Zip(actualArray).All(results => results.First.CheckEqual(results.Second));
            }

            return expectedArray.All(expectedResult => actualArray.Any(expectedResult.CheckEqual));
        }

        public static bool CheckEqual(this IQuestionReference expected, IQuestionReference actual)
        {
            return expected.ChoiceId == actual.ChoiceId && expected.QuestionId == actual.QuestionId;
        }

        public static bool CheckEqual(this IChoice expected, IChoice actual)
        {
            return expected.Selectable.CheckEqual(actual.Selectable) &&
                   expected.Answer.CheckEqual(actual.Answer) &&
                   expected.Id.CheckEqual(actual.Id) &&
                   expected.Order.CheckEqual(actual.Order);
        }

        public static bool CheckEqual(this IQuestion expected, IQuestion actual)
        {
            return expected.Choices.CheckEqual(actual.Choices, true) &&
                   expected.Order.CheckEqual(actual.Order) &&
                   expected.Text.CheckEqual(actual.Text) &&
                   expected.Id.CheckEqual(actual.Id);
        }

        public static bool CheckEqual(this ISurveyResult expected, ISurveyResult actual)
        {
            return expected.CheckEqualBase(actual) &&
                   expected.IsSuggested.CheckEqual(actual.IsSuggested) &&
                   expected.Results.CheckEqual(actual.Results, true) &&
                   expected.ParticipantId.CheckEqual(actual.ParticipantId);
        }

        public static bool CheckEqual(this DateTime expected, DateTime actual)
        {
            return expected == actual;
        }

        public static bool CheckEqual(this string? expected, string? actual)
        {
            return expected == actual;
        }

        public static bool CheckEqual(this bool expected, bool actual)
        {
            return expected == actual;
        }

        public static bool CheckEqual(this int expected, int actual)
        {
            return expected == actual;
        }

        public static bool CheckEqual(this ISurvey? expected, ISurvey? actual)
        {
            if (expected == null && actual == null)
            {
                return true;
            }

            if (expected == null || actual == null)
            {
                return false;
            }

            return expected.CheckEqualBase(actual) &&
                   expected.Participants.CheckEqual(actual.Participants, false) &&
                   expected.Info.CheckEqual(actual.Info) &&
                   expected.Link.CheckEqual(actual.Link) &&
                   expected.Name.CheckEqual(actual.Name) &&
                   expected.Organizer.CheckEqual(actual.Organizer) &&
                   expected.Questions.CheckEqual(actual.Questions, true);
        }

        public static bool CheckEqual(this IParticipant expected, IParticipant actual)
        {
            return expected.QuestionReferences.CheckEqual(actual.QuestionReferences, true) &&
                   expected.Name.CheckEqual(actual.Name) &&
                   expected.Email.CheckEqual(actual.Email) &&
                   expected.Id.CheckEqual(actual.Id) &&
                   expected.Order.CheckEqual(actual.Order);
        }

        public static bool CheckEqual(this IPerson expected, IPerson actual)
        {
            return expected.Name.CheckEqual(actual.Name) &&
                   expected.Email.CheckEqual(actual.Email) &&
                   expected.Id.CheckEqual(actual.Id);
        }

        public static bool CheckEqual(this ISavePlayerMappingsMessage expected, ISavePlayerMappingsMessage actual)
        {
            return expected.PlayerMappings.CheckEqual(actual.PlayerMappings) &&
                   expected.ProcessId.CheckEqual(actual.ProcessId);
        }

        public static bool CheckEqual(this IPlayerMappings expected, IPlayerMappings actual)
        {
            return expected.CheckEqualBase(actual) &&
                   expected.PlayerCountryMappings.CheckEqual(actual.PlayerCountryMappings, false);
        }

        public static bool CheckEqual(this IStartGameMessage expected, IStartGameMessage actual)
        {
            return expected.GameSeriesDocumentId.CheckEqual(actual.GameSeriesDocumentId) &&
                   expected.ProcessId.CheckEqual(actual.ProcessId);
        }

        public static bool CheckEqual(
            this IEnumerable<IPlayerCountryMapping> expected,
            IEnumerable<IPlayerCountryMapping> actual,
            bool ordered
        )
        {
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            if (expectedArray.Length != actualArray.Length)
            {
                return false;
            }

            if (ordered)
            {
                return expectedArray.Zip(actualArray).All(results => results.First.CheckEqual(results.Second));
            }

            return expectedArray.All(expectedResult => actualArray.Any(expectedResult.CheckEqual));
        }

        public static bool CheckEqual(this IPlayerCountryMapping expected, IPlayerCountryMapping actual)
        {
            return expected.CountryId.CheckEqual(actual.CountryId) && expected.PlayerId.CheckEqual(actual.PlayerId);
        }

        public static bool CheckEqual(this ISendMailMessage expected, ISendMailMessage actual)
        {
            return expected.Body.CheckEqual(actual.Body) &&
                   expected.Recipients.CheckEqual(actual.Recipients, false) &&
                   expected.ReplyTo.CheckEqual(actual.ReplyTo) &&
                   expected.Subject.CheckEqual(actual.Subject) &&
                   expected.ProcessId.CheckEqual(actual.ProcessId);
        }

        public static bool CheckEqual(this IBody expected, IBody actual)
        {
            return expected.Html.CheckEqual(actual.Html) && expected.Plain.CheckEqual(actual.Html);
        }

        public static bool CheckEqual(
            this IEnumerable<IRecipient> expected,
            IEnumerable<IRecipient> actual,
            bool ordered
        )
        {
            var expectedArray = expected.ToArray();
            var actualArray = actual.ToArray();

            if (expectedArray.Length != actualArray.Length)
            {
                return false;
            }

            if (ordered)
            {
                return expectedArray.Zip(actualArray).All(results => results.First.CheckEqual(results.Second));
            }

            return expectedArray.All(expectedResult => actualArray.Any(expectedResult.CheckEqual));
        }

        public static bool CheckEqual(this IRecipient expected, IRecipient actual)
        {
            return expected.Name.CheckEqual(actual.Name) && expected.Email.CheckEqual(actual.Email);
        }

        public static bool CheckEqual(this ISaveSurveyStatusMessage expected, ISaveSurveyStatusMessage actual)
        {
            return expected.SurveyStatus.CheckEqual(actual.SurveyStatus) &&
                   expected.ProcessId.CheckEqual(actual.ProcessId);
        }

        public static bool CheckEqual(this ISurveyStatus expected, ISurveyStatus actual)
        {
            return expected.CheckEqualBase(actual) &&
                   expected.ParticipantId.CheckEqual(actual.ParticipantId) &&
                   expected.Status.CheckEqual(actual.Status);
        }

        public static bool CheckEqual(this Status expected, Status actual)
        {
            return expected == actual;
        }

        public static bool CheckEqual(this ISaveGameSeriesMessage expected, ISaveGameSeriesMessage actual)
        {
            return expected.ProcessId.CheckEqual(actual.ProcessId) && expected.GameSeries.CheckEqual(actual.GameSeries);
        }

        public static bool CheckEqual(this IGameSeries expected, IGameSeries actual)
        {
            throw new NotImplementedException();
        }

        public static bool CheckEqualBase(this IDatabaseObject expected, IDatabaseObject actual)
        {
            return expected.DocumentId.CheckEqual(actual.DocumentId) &&
                   expected.Created.CheckEqual(actual.Created) &&
                   expected.ParentDocumentId.CheckEqual(actual.ParentDocumentId);
        }
    }
}
