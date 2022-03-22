namespace Md.Tga.InitializeGameSeriesSubscriber.Tests.Mocks
{
    using System;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Surveys.Common.Contracts.Messages;

    internal class FunctionProviderMock : IPubSubProvider<ISurveyClosedMessage>
    {
        private readonly ISurveyClosedMessage expectedMessage;

        public FunctionProviderMock(ISurveyClosedMessage expectedMessage)
        {
            this.expectedMessage = expectedMessage;
        }

        public Task HandleAsync(ISurveyClosedMessage message)
        {
            return Task.CompletedTask;
        }

        public Task LogErrorAsync(Exception ex, string message)
        {
            return Task.CompletedTask;
        }
    }
}
