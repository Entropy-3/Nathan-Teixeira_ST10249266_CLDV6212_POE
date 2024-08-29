using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CLDV_SEM2_POE.Services
{
    public class QueueService
    {
        private readonly QueueServiceClient queueServiceClient;

        public QueueService(IConfiguration configuration)
        {
            queueServiceClient = new QueueServiceClient(configuration["AzureStorage:ConnectionString"]);
        }

        //-------------------------------------------------------------------------------------------------------\\
        //method that logs the order being processed to a queue
        public async Task SendMessageAsync(string queueName, string message)
        {
            var queueClient = queueServiceClient.GetQueueClient(queueName);
            await queueClient.CreateIfNotExistsAsync();
            await queueClient.SendMessageAsync(message);
        }
    }
}
//------------------------------------------eof-------------------------------------------------------------\\