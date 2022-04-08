namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System;
    using System.Threading.Tasks;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    public class SurveyClosedPubSubClientMock : ISurveyClosedPubSubClient
    {
        private readonly ISurveyClosedMessage? expected;

        public SurveyClosedPubSubClientMock()
            : this(null)
        {
        }

        public SurveyClosedPubSubClientMock(ISurveyClosedMessage? expected)
        {
            this.expected = expected;
        }

        public int CallCounter { get; private set; }

        public async Task PublishAsync(ISurveyClosedMessage message)
        {
            this.CallCounter += 1;
            if (this.expected != null && !this.expected.CheckEqual(message))
            {
                throw new ArgumentException("message mismatch");
            }

            await Task.CompletedTask;
        }
    }
}
