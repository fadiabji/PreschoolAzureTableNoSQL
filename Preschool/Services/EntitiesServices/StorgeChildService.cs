using Azure.Data.Tables;
using Newtonsoft.Json;
using NuGet.Common;
using Preschool.Models.Entities;
using System;
using System.Configuration;

namespace Preschool.Services.EntitiesServices
{
    public class StorgeChildService : IStorgeChildService
    {
        private readonly IConfiguration _configuration;
        private readonly TableServiceClient _tableServiceClient;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();
        public StorgeChildService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tableServiceClient = new TableServiceClient(_configuration["AzureWebJobStorge"]);
        }

        public async void AddChildToTable(ChildEntity childEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ChildernTable");
            tableClient.CreateIfNotExists();
            childEntity.Id = random.Next(0, 10000000);
            // how to insure the id could not the same by coinsident?
            childEntity.RowKey = childEntity.Id.ToString();
            //childEntity.RowKey = new string(Enumerable.Repeat(chars, 20)
            //            .Select(s => s[random.Next(s.Length)]).ToArray());
            await tableClient.AddEntityAsync(childEntity);
        }

        public List<ChildEntity> GetChildEntities()
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ChildernTable");
            tableClient.CreateIfNotExists();
            var list = tableClient.Query<ChildEntity>().ToList();
            return list;
        }


        public ChildEntity GetChildEntityById(int? id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ChildernTable");
            tableClient.CreateIfNotExists();
            var childEntity = tableClient.Query<ChildEntity>(c => c.RowKey == id.ToString()).FirstOrDefault();
            return childEntity;
        }



        public void DeleteChildEntity(ChildEntity childEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ChildernTable");
            tableClient.CreateIfNotExists();
            var tableEntity = tableClient.Query<ChildEntity>(c => c.RowKey == childEntity.RowKey).FirstOrDefault();

            tableClient.DeleteEntity(tableEntity.PartitionKey, tableEntity.RowKey);

        }

        public void UpdateChildEntity(ChildEntity childEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ChildernTable");
            tableClient.CreateIfNotExists();
            var tableEntity = tableClient.Query<ChildEntity>(c => c.RowKey == childEntity.RowKey).FirstOrDefault();
            childEntity.ETag = tableEntity.ETag;
            tableClient.UpdateEntity(childEntity, childEntity.ETag);
        }


        public bool IsChildExists(int id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "ChildernTable");
            return tableClient.Query<ChildEntity>().Any(c => c.Id == id);
        }

    }
}
