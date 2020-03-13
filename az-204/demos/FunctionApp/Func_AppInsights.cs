using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.ApplicationInsights;
using System.Diagnostics;

namespace FunctionApp
{
    public static class Func_AppInsights
    {
        [FunctionName("Func_AppInsights")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //Application Insights
            // Setup Telemetry
            TelemetryClient telemetry = new TelemetryClient();
            telemetry.InstrumentationKey = System.Environment.GetEnvironmentVariable("APP_INSIGHTS_KEY");
            String requestname = req.Path.Value;
            var time = DateTime.Now;
            var sw = Stopwatch.StartNew();
            telemetry.Context.Operation.Id = Guid.NewGuid().ToString();

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            //code right here...takes 400sec

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";
            
            //send result to App Insights
            telemetry.TrackRequest(requestname, time, sw.Elapsed, "200", true);

            return new OkObjectResult(responseMessage);
        }
    }
}
