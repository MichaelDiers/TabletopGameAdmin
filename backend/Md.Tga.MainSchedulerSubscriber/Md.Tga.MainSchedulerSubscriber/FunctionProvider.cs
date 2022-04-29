namespace Md.Tga.MainSchedulerSubscriber
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Md.Common.Messages;
    using Md.Tga.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : IFunctionProvider
    {
        /// <summary>
        ///     Clients for sending pub/sub messages to schedulers.
        /// </summary>
        private readonly IEnumerable<ISchedulerPubSubClient> schedulerPubSubClients;

        /// <summary>
        ///     Initializes a new instance of the FunctionProvider class.
        /// </summary>
        /// <param name="schedulerPubSubClients">Clients for sending pub/sub messages to schedulers.</param>
        public FunctionProvider(IEnumerable<ISchedulerPubSubClient> schedulerPubSubClients)
        {
            this.schedulerPubSubClients = schedulerPubSubClients;
        }

        /// <summary>
        ///     The method is executed for each incoming request.
        /// </summary>
        /// <returns>A <see cref="Task" /> whose result indicates termination.</returns>
        public Task HandleAsync()
        {
            var processId = Guid.NewGuid().ToString();
            var results = this.schedulerPubSubClients.Select(client => client.PublishAsync(new Message(processId)))
                .ToArray();
            Task.WaitAll(results);
            return Task.CompletedTask;
        }
    }
}
