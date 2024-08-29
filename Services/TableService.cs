﻿using Azure;
using Azure.Data.Tables;
using CLDV_SEM2_POE.Models;
using System.Threading.Tasks;

namespace CLDV_SEM2_POE.Services
{
    public class TableService
    {
        private readonly TableClient _tableClient;

        public TableService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureStorage:ConnectionString"];
            var serviceClient = new TableServiceClient(connectionString);
            _tableClient = serviceClient.GetTableClient("CustomerProfiles");
            _tableClient.CreateIfNotExists();
        }

        //-------------------------------------------------------------------------------------------------------\\
        //method that adds a customer profile to the table
        public async Task AddEntityAsync(CustomerProfile profile)
        {
            await _tableClient.AddEntityAsync(profile);
        }
    }
}
//------------------------------------------eof-------------------------------------------------------------\\