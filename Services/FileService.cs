using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;


namespace CLDV_SEM2_POE.Services
{
    public class FileService
    {

        private readonly ShareServiceClient shareServiceClient;

        public FileService(IConfiguration configuration)
        {
            shareServiceClient = new ShareServiceClient(configuration["AzureStorage:ConnectionString"]);
        }

        //-------------------------------------------------------------------------------------------------------\\
        //method that uploads a file to file storage
        public async Task UploadFileAsync(string shareName, string fileName, Stream content)
        {
            var shareClient = shareServiceClient.GetShareClient(shareName);
            await shareClient.CreateIfNotExistsAsync();
            var directoryClient = shareClient.GetRootDirectoryClient();
            var fileClient = directoryClient.GetFileClient(fileName);
            await fileClient.CreateAsync(content.Length);
            await fileClient.UploadAsync(content);
        }
    }
}
//------------------------------------------eof-------------------------------------------------------------\\