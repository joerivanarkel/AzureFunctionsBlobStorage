using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Blob
{
    public class ReadBlobStorage
    {
        [FunctionName("ReadBlobStorage")]
        public void Run
        (
            [BlobTrigger("blobtest/{name}", Connection = "huhaaaha_STORAGE")]Stream myBlob, string name, ILogger log
        )
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}
