namespace Md.Tga.Common.TestData.Mocks
{
    using System.Collections.Generic;
    using System.Linq;
    using Surveys.Common.Contracts;
    using Surveys.Common.Contracts.Messages;

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
                   expected.Results.CheckEqual(actual.Results, true) &&
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
            return expected.IsSuggested.CheckEqual(actual.IsSuggested) &&
                   expected.Results.CheckEqual(actual.Results, true) &&
                   expected.InternalSurveyId.CheckEqual(actual.InternalSurveyId) &&
                   expected.ParticipantId.CheckEqual(actual.ParticipantId);
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

            return expected.Participants.CheckEqual(actual.Participants, false) &&
                   expected.Info.CheckEqual(actual.Info) &&
                   expected.Link.CheckEqual(actual.Link) &&
                   expected.Name.CheckEqual(actual.Name) &&
                   expected.Organizer.CheckEqual(actual.Organizer) &&
                   expected.Questions.CheckEqual(actual.Questions, true) &&
                   expected.Id.CheckEqual(actual.Id);
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
    }
}
