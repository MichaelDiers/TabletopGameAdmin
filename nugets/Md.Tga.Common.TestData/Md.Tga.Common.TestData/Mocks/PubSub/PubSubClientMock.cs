namespace Md.Tga.Common.TestData.Mocks.PubSub
{
    using System;
    using System.Threading.Tasks;

    public abstract class PubSubClientMock<T> where T : class
    {
        protected PubSubClientMock()
            : this(null)
        {
        }

        protected PubSubClientMock(T? expectedMessage)
        {
            this.ExpectedMessage = expectedMessage;
        }

        public int CallCounter { get; set; }

        public T? ExpectedMessage { get; set; }

        public async Task PublishAsync(T message)
        {
            this.CallCounter += 1;
            if (this.ExpectedMessage != null && !await this.CheckMessage(this.ExpectedMessage, message))
            {
                throw new ArgumentException("Message mismatch!");
            }
        }

        protected abstract Task<bool> CheckMessage(T expected, T actual);
    }
}
