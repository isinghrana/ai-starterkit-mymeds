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
using System.Collections.Generic;
using Azure;
using Azure.AI.TextAnalytics;
namespace GetMedInfoFromTextFuncApp
{
    /// <summary>
    /// Azure Function identifies the Medicine information from text using an external Api referred as 
    /// MedApi (yet to be implemented).
    /// ****This Azure Function is a place holder at this point****
    /// </summary>
    public static class GetMedInfoFromTextFunc
    {
        private const string TEXTANALYTICS_ENDPOINT = "TextAnalyticsEndpoint";
        private const string TEXTANALYTICS_KEY = "TextAnalyticsKey";

        [FunctionName("GetMedInfoFromText")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string inputText = await req.Content.ReadAsStringAsync();
            log.LogInformation($"Input Text Received - {inputText}");

            string endpoint = GetEnvVarValue(TEXTANALYTICS_ENDPOINT);
            string apiKey = GetEnvVarValue(TEXTANALYTICS_KEY);
            var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

            List<string> batchInput = new List<string>()
            {
                inputText
            };

            AnalyzeHealthcareEntitiesOperation healthOperation = await client.StartAnalyzeHealthcareEntitiesAsync(batchInput);
            await healthOperation.WaitForCompletionAsync();

            log.LogInformation($"Created On   : {healthOperation.CreatedOn}");
            log.LogInformation($"Expires On   : {healthOperation.ExpiresOn}");
            log.LogInformation($"Status       : {healthOperation.Status}");
            log.LogInformation($"Last Modified: {healthOperation.LastModified}");

            List<JsonResult> entityOutput = new List<JsonResult>();

            await foreach (AnalyzeHealthcareEntitiesResultCollection documentsInPage in healthOperation.Value)
            {
                log.LogInformation($"Results of Azure Text Analytics \"Healthcare Async\" Model, version: \"{documentsInPage.ModelVersion}\"");
                log.LogInformation("");

                foreach (AnalyzeHealthcareEntitiesResult entitiesInDoc in documentsInPage)
                {
                    if (!entitiesInDoc.HasError)
                    {
                        foreach (var entity in entitiesInDoc.Entities)
                        {
                            // view recognized healthcare entities
                            log.LogInformation($"  Entity: {entity.Text}");
                            log.LogInformation($"  Category: {entity.Category}");
                            log.LogInformation($"  Offset: {entity.Offset}");
                            log.LogInformation($"  Length: {entity.Length}");
                            log.LogInformation($"  NormalizedText: {entity.NormalizedText}");
                            log.LogInformation($"  Links:");

                            JsonResult entityJson = new JsonResult(new TA4HEntityOutput()
                            {
                                Entity= $"{entity.Text}",
                                Category= $"{entity.Category}",
                                NormalizedText= $"{entity.NormalizedText}"
                            }
                                );

                            entityOutput.Add(entityJson);
                        }
                    }
                    else
                    {
                        log.LogInformation("  Error!");
                        log.LogInformation($"  Document error code: {entitiesInDoc.Error.ErrorCode}.");
                        log.LogInformation($"  Message: {entitiesInDoc.Error.Message}");
                    }

                    log.LogInformation("");
                }
            }
            string finalJson = JsonConvert.SerializeObject(entityOutput, Formatting.Indented);
            return new JsonResult(finalJson);
        }

        private static string GetEnvVarValue(string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }

    #region Helper Classes

    /// <summary>
    /// Class for the Ouput returned by this Azure Function
    /// </summary>
    public class MedInfoFromTextOutput
    {
        public string MedName { get; set; }
        public double MedNameScore { get; set; }

        public string MedDosage { get; set; }
        public double MedDosageScore { get; set; }

        public string MedFrequency { get; set; }
        public double MedFrequencyScore { get; set; }
    }

    public class TA4HEntityOutput
    {
        public string Entity { get; set; }
        public string Category { get; set; }

        public string NormalizedText { get; set; }
    }


    /// <summary>
    /// Input for the external Med Api
    /// </summary>
    public class MedApiInputMessage
    {
        public List<MedApiInputDoc> Docs { get; set; }
    }

    public class MedApiInputDoc
    {
        public string Language { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
    }

    //Output from the external Med Api
    public class MedApiOutputMessage
    {
        public List<MedApiOutputDoc> Docs { get; set; }
    }

    public class MedApiOutputDoc
    {
        public string Id { get; set; }
        public List<Entity> Entities { get; set; }
    }

    public class Entity
    {
        public int Offset { get; set; }
        public int Length { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public double Score { get; set; }
    }
    #endregion

}
