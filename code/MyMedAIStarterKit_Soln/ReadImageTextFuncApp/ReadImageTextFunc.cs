using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Text;

namespace ReadImageTextFuncApp
{
    public static class ReadImageTextFunc
    {
        private const string STORAGE_CONNECTIONSTRING = "StorageConnectionString";
        private const string COGNITIVESERVICE_KEY = "CognitiveServiceKey";
        private const string COGNITIVESERVICE_ENDPOINT = "CognitiveServiceEndpoint";
        
        [FunctionName("ReadImageText")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            ImageLocationInfo imageLocationInfo = await req.Content.ReadAsAsync<ImageLocationInfo>();

            if (imageLocationInfo == null || String.IsNullOrWhiteSpace(imageLocationInfo.BlobUrl))
                return new BadRequestObjectResult("BlobUrl must be provided in the body of HttpPost");

            log.LogInformation($"ImageLocation: {imageLocationInfo.BlobUrl}");

            string imageText = "";
            byte[] imageData = await GetBlobData(imageLocationInfo.BlobUrl, log);

            using (Stream stream = new MemoryStream(imageData))
            {
                log.LogDebug("Stream Length: " + stream.Length);
                imageText = await GetTextFromImage(stream, log);
            }

            return new JsonResult(new ReadImageTextResult() { ImageText = imageText });
        }

        private static async Task<byte[]> GetBlobData(string blobUrl, ILogger logger)
        {
            string storageConnectionString = GetEnvVarValue(STORAGE_CONNECTIONSTRING);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient storageClient = storageAccount.CreateCloudBlobClient();            

            var blobRef = await storageClient.GetBlobReferenceFromServerAsync(new Uri(blobUrl)); 
            logger.LogInformation("BlobRef Object Type: " + blobRef.GetType().FullName);
            blobRef.FetchAttributes();
            byte[] imageData = new byte[blobRef.Properties.Length];

            await blobRef.DownloadToByteArrayAsync(imageData, 0);
            return imageData;
        }

        private static async Task<string> GetTextFromImage(Stream imageStream, ILogger logger)
        {            
            string cognitiveServiceApiKey = GetEnvVarValue(COGNITIVESERVICE_KEY);
            string cognitiveServiceEndpoint = GetEnvVarValue(COGNITIVESERVICE_ENDPOINT);

            ComputerVisionClient computerVisionClient = new ComputerVisionClient(
                      new ApiKeyServiceClientCredentials(cognitiveServiceApiKey),
                      new System.Net.Http.DelegatingHandler[] { });
            computerVisionClient.Endpoint = cognitiveServiceEndpoint;

            // Start the async process to recognize the text
            BatchReadFileInStreamHeaders textHeaders =
                await computerVisionClient.BatchReadFileInStreamAsync(imageStream);

            var t = GetTextAsync(computerVisionClient, textHeaders.OperationLocation);
            t.Wait();
            return t.Result;
        }

        private static async Task<string> GetTextAsync(ComputerVisionClient computerVision,
                                                            string operationLocation)
        {
            int numberOfCharsInOperationId = 36;
            // Retrieve the URI where the recognized text will be
            // stored from the Operation-Location header
            string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

            ReadOperationResult result = await computerVision.GetReadOperationResultAsync(operationId);

            // Wait for the operation to complete
            int i = 0;
            int maxRetries = 10;
            while ((result.Status == TextOperationStatusCodes.Running ||
                    result.Status == TextOperationStatusCodes.NotStarted) && i++ < maxRetries)
            {
                //Console.WriteLine("Server status: {0}, waiting {1} seconds...", result.Status, i);
                await Task.Delay(1000);
                result = await computerVision.GetReadOperationResultAsync(operationId);
            }

            //TODO: Improve Error Handling - Case where Max Tries exceed for Result.STatus is failure

            // Display the results            
            var recResults = result.RecognitionResults;
            StringBuilder imageText = new StringBuilder();
            foreach (TextRecognitionResult recResult in recResults)
            {
                foreach (Line line in recResult.Lines)
                {
                    imageText.Append(line.Text);
                    imageText.Append(" ");
                }
            }
            return imageText.ToString();
        }

        private static string GetEnvVarValue(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }

    public class ImageLocationInfo
    {
        public string BlobUrl { get; set; }
    }

    public class ReadImageTextResult
    {
        public string ImageText { get; set; }
    }
}
