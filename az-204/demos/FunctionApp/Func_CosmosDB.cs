using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public static class Func_CosmosDB
    {
        [FunctionName("Func_CosmosDB")]
        public static void Run([CosmosDBTrigger(
            databaseName: "database1",
            collectionName: "container1",
            ConnectionStringSetting = "EmployeeDBConnection",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
