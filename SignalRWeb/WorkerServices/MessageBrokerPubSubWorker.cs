using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SignalRWeb.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace SignalRWeb.WorkerServices
{
    [Route("api/messagebroker")]
    public sealed class MessageBrokerPubSubWorker : BackgroundService
    {
        private IHubContext<MessageBrokerHub> _messageBrokerHubContext;

        public MessageBrokerPubSubWorker(IHubContext<MessageBrokerHub> messageBrokerHubContext) {
            _messageBrokerHubContext = messageBrokerHubContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested) {
                await Task.Delay(1000);
                var proc = Process.GetCurrentProcess();
                var mem = proc.WorkingSet64;
                var cpu = proc.TotalProcessorTime;
                var eventMessage = new EventMessage("memory => " + mem + " | Procesor => "+ cpu.TotalSeconds.ToString(), $"{Guid.NewGuid():N}", DateTime.Now);
                await _messageBrokerHubContext.Clients.All.SendAsync("onMessageReceived",eventMessage,stoppingToken);
            }
        }

    }
}
