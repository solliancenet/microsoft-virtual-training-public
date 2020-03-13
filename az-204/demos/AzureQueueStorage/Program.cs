using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureQueueStorage
{
    class Program
    {
        //https://docs.microsoft.com/en-us/azure/storage/queues/storage-dotnet-how-to-use-queues
        static void Main(string[] args)
        {
            string conn = "YOURCONN";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(conn);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage("Hello, World");
            queue.AddMessage(message);

            // Peek at the next message
            CloudQueueMessage peekedMessage = queue.PeekMessage();

            // Display message.
            Console.WriteLine(peekedMessage.AsString);
        }
    }
}
