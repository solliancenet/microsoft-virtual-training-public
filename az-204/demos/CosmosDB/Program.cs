using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CosmosDB
{
    //https://docs.microsoft.com/en-us/azure/cosmos-db/create-sql-api-dotnet
    class Program
    {
        static Database database;
        static Container container;

        static async Task Main(string[] args)
        {
            string cosmosUrl = ConfigurationManager.AppSettings["COSMOSURL"]; ;
            string cosmosKey = ConfigurationManager.AppSettings["COSMOSKEY"]; ;
            string dbId = ConfigurationManager.AppSettings["DBID"]; ;
            string containerId = ConfigurationManager.AppSettings["CONTAINERID"]; ;

            // Create new CosmosClient to communiciate with Azure Cosmos DB
            using (var cosmosClient = new CosmosClient(cosmosUrl, cosmosKey))
            {
                // Create new database
                database = await cosmosClient.CreateDatabaseIfNotExistsAsync(dbId);

                // Create new container
                container = await database.CreateContainerIfNotExistsAsync(containerId, "/PartitionKey");

                string familyId = Guid.NewGuid().ToString();

                // Add item to container
                var todoItem = new TodoItem()
                {
                    id = Guid.NewGuid().ToString(),
                    PartitionKey = familyId,
                    Task = "Get started with Azure Cosmos DB!"
                };

                var todoItemResponse = await container.CreateItemAsync<Item>(todoItem, new PartitionKey(todoItem.PartitionKey));

                var family = new Family()
                {
                    LastName = "Anderson",
                    Id = Guid.NewGuid().ToString(),
                    PartitionKey = familyId
                };

                var familyResponse = await container.CreateItemAsync<Family>(family);

                await QueryItemsAsync(); 

                //parition key of family lastname?  Why not?  Smith anyone?
            }
        }

        static private async Task QueryItemsAsync()
        {
            var sqlQueryText = "SELECT * FROM c WHERE c.LastName = 'Anderson'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Family> queryResultSetIterator = container.GetItemQueryIterator<Family>(queryDefinition);

            List<Family> families = new List<Family>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Family> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Family family in currentResultSet)
                {
                    families.Add(family);
                    Console.WriteLine("\tRead {0}\n", family);
                }
            }
        }
    }
}
