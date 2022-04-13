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
    public static class HttpTriggerFileToBlob
    {
        [FunctionName("HttpTriggerFileToBlob")]
        public static async Task Run
        (
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Blob("blobtest/httptrigger.txt",FileAccess.Write,Connection = "AzureWebJobsStorage")] Stream blob,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                var formdata = await req.ReadFormAsync();
                var files = formdata.Files["file"];
                var fileBytes = await GetBytes(files);

                blob.Write(fileBytes, 0, fileBytes.Length);

                log.LogInformation("File successfully uploaded to blob");
            }
            catch (System.Exception)
            {
                log.LogInformation("File failed to upload to blob");
                throw;
            }
            
        }

        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
