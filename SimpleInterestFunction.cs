using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LoanWizard
{
    public static class SimpleInterestFunction
    {
        [FunctionName("SimpleInterest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            // Parse the query parameters: principal, rate, term
            string principalQuery = req.Query["principal"];
            string rateQuery = req.Query["rate"];
            string termQuery = req.Query["term"];

            // Convert to float values
            if (!float.TryParse(principalQuery, out float principal) ||
                !float.TryParse(rateQuery, out float rate) ||
                !float.TryParse(termQuery, out float term))
            {
                // If any value is missing or not a valid number, return a 400 error
                return new BadRequestObjectResult("Please supply valid principal, rate, and term in the query string.");
            }

            // Calculate the simple interest
            float simpleInterest = principal * rate * term;

            // Return the result
            return new OkObjectResult(simpleInterest);

            //string name = req.Query["name"];
            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;
            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";
            //return new OkObjectResult(responseMessage);
        }
    }
}
