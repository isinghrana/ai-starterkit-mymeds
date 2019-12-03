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

namespace GetMedInfoFromTextFuncApp
{
    /// <summary>
    /// Azure Function identifies the Medicine information from text using an external Api referred as 
    /// MedApi (yet to be implemented).
    /// ****This Azure Function is a place holder at this point****
    /// </summary>
    public static class GetMedInfoFromTextFunc
    {
        [FunctionName("GetMedInfoFromText")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string inputText = await req.Content.ReadAsStringAsync();
            log.LogInformation($"Input Text Received - {inputText}");
                        
            //TODO: For now return hard-coded value, once the External Api for Med Info detection is 
            //      implemented this function can be updated 
            /*if no info identified then 
            {
                return new NotFoundObjectResult("No Medication Label identified in the text");
            }
            else*/
            //{
                return new JsonResult(new MedInfoFromTextOutput()
                {
                    MedName = "Nexium",
                    MedNameScore = .95,
                    MedDosage = "1 Tablet",
                    MedDosageScore = .75,
                    MedFrequency = "Twice Daily",
                    MedFrequencyScore = .87
                }                    
                    );
            //}
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
