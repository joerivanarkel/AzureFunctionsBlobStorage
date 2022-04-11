using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Storage.Blob;

namespace BlobInput
{
    public static class HttpTriggerTextToBlob
    {
        [FunctionName("HttpTriggerTextToBlob")]
        public static async Task<IActionResult> Run
        (
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Blob("blobtest/httptrigger.txt",FileAccess.Write,Connection = "AzureWebJobsStorage")]TextWriter report,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "- This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"- Hello, {name}. This HTTP triggered function executed successfully.";

            await report.WriteLineAsync(responseMessage);

            return new OkObjectResult(responseMessage);
        }
    }
}
