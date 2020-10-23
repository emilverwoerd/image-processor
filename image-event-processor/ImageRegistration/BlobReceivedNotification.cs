using System;
using System.IO;
using ImageRegistration.Models;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

namespace ImageRegistration
{
    public static class BlobReceivedNotification
    {
        [FunctionName("BlobReceivedNotification")]
        [return: EventGrid(TopicEndpointUri = "EventGridTopicUriSetting", TopicKeySetting = "EventGridTopicKeySetting")]
        public static EventGridEvent Run([BlobTrigger("images/{name}", Connection = "BlogImageStorageConnection")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            return new EventGridEvent(
                Guid.NewGuid().ToString("D"),
                "new-image-received",
                new ImageData()
                {
                    Name = name,
                    Url = $"http://127.0.0.1:10000/devstoreaccount1/images/{name}",
                    Size = myBlob.Length
                },
                "com.serverless.event",
                DateTime.UtcNow,
                "1.0");
        }
    }
}
