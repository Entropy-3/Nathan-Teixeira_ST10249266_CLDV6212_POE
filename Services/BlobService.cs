using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace CLDV_SEM2_POE.Services
{
    public class BlobService
    {
        private readonly BlobServiceClient blobServiceClient;

        public BlobService(IConfiguration configuration)
        {
            blobServiceClient = new BlobServiceClient(configuration["AzureStorage:ConnectionString"]);
        }

        //-------------------------------------------------------------------------------------------------------\\
        //method that uploads an image to blob storage
        public async Task UploadBlobAsync(string containerName, string blobName, Stream content)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(content, true);
        }
    }
}
//------------------------------------------eof-------------------------------------------------------------\\