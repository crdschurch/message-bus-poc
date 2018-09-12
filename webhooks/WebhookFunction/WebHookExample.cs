using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using AzureFunctionSample.Services;

namespace AzureFunctionSample
{
    public static class WebHookExample
    {
        private static IMessageQueueService _messageQueueService = new MessageQueueService();

        [FunctionName("WebHookExample")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processing a request.");

            try
            {
                string body = await req.Content.ReadAsStringAsync();
                if (!String.IsNullOrEmpty(body))
                {
                    _messageQueueService.AddMessageToQueue(body);
                }

                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                log.Error($"Exception: {e.Message}", e);
                return req.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
