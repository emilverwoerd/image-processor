// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace ImageNotification
{
    public static class ImageEventReceivedNotification
    {
        [FunctionName("ImageReceivedNotification")]
        public static async void Run([EventGridTrigger]EventGridEvent eventGridEvent, ILogger log,
            [SignalR(HubName = "hubName")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            log.LogInformation(eventGridEvent.Data.ToString());

            await signalRMessages.AddAsync(new SignalRMessage
            {
                /// UserId optional
                Target = "newImage",
                Arguments = new[] { eventGridEvent.Data }
            });
        }
    }
}
