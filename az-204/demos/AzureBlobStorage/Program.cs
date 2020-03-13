using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABS
{
    //https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet-legacy
    class Program
    {
        static void Main(string[] args)
        {
            string storageConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
            
            MainAsync(storageConnectionString).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string storageConnectionString)
        {
            // Check whether the connection string can be parsed.
            CloudStorageAccount storageAccount;

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                // Create the CloudBlobClient that represents the 
                // Blob storage endpoint for the storage account.
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                // Create a container called 'quickstartblobs' and 
                // append a GUID value to it to make the name unique.
                CloudBlobContainer cloudBlobContainer =
                    cloudBlobClient.GetContainerReference("quickstartblobs" +
                        Guid.NewGuid().ToString());

                await cloudBlobContainer.CreateAsync();

                // Create a file in your local MyDocuments folder to upload to a blob.
                string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string localFileName = "QuickStart_" + Guid.NewGuid().ToString() + ".txt";
                string sourceFile = Path.Combine(localPath, localFileName);
                
                // Write text to the file.
                File.WriteAllText(sourceFile, "Hello, World!");

                Console.WriteLine("Temp file = {0}", sourceFile);
                Console.WriteLine("Uploading to Blob storage as blob '{0}'", localFileName);

                // Get a reference to the blob address, then upload the file to the blob.
                // Use the value of localFileName for the blob name.
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);

                await cloudBlockBlob.UploadFromFileAsync(sourceFile);
            }
            else
            {
                // Otherwise, let the user know that they need to define the environment variable.
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add an environment variable named 'AZURE_STORAGE_CONNECTION_STRING' with your storage " +
                    "connection string as a value.");
                Console.WriteLine("Press any key to exit the application.");
                Console.ReadLine();
            }
        }
    }
}
