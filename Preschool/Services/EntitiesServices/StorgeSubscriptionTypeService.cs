using Azure.Data.Tables;
using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public class StorgeSubscriptionTypeService : IStorgeSubscriptionTypeService
    {
        private readonly IConfiguration _configuration;
        private readonly TableServiceClient _tableServiceClient;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();
        
        public StorgeSubscriptionTypeService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tableServiceClient = new TableServiceClient(_configuration["AzureWebJobStorge"]);
        }


        public async void AddSubscriptionTypeToTable(SubscriptionTypeEntity subscription)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionTypsTable");
            tableClient.CreateIfNotExists();
            subscription.Id = random.Next(0, 10000000);
            subscription.RowKey = subscription.Id.ToString();
            await tableClient.AddEntityAsync(subscription);
        }

        public void DeleteSubscriptionTypeEntity(SubscriptionTypeEntity subscriptionTypeEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionTypsTable");
            tableClient.CreateIfNotExists();
            var tableEntity = tableClient.Query<SubscriptionTypeEntity>(c => c.RowKey == subscriptionTypeEntity.RowKey).FirstOrDefault();

            tableClient.DeleteEntity(tableEntity.PartitionKey, tableEntity.RowKey);
        }

        public List<SubscriptionTypeEntity> GetSubscriptionTypeEntities()
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionTypsTable");
            tableClient.CreateIfNotExists();
            var list = tableClient.Query<SubscriptionTypeEntity>().ToList();
            return list;
        }

        public SubscriptionTypeEntity GetSubscriptionTypeEntityById(int? id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionTypsTable");
            tableClient.CreateIfNotExists();
            var teacherEntity = tableClient.Query<SubscriptionTypeEntity>(c => c.RowKey == id.ToString()).FirstOrDefault();
            return teacherEntity;
        }

        public bool IsSubscriptionTypeExists(int id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionTypsTable");
            return tableClient.Query<SubscriptionTypeEntity>().Any(s => s.Id == id);

        }

        public void UpdateSubscriptionTypeEntity(SubscriptionTypeEntity subscriptionTypeEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionTypsTable");
            tableClient.CreateIfNotExists();
            var tableEntity = tableClient.Query<SubscriptionTypeEntity>(s => s.RowKey == subscriptionTypeEntity.RowKey).FirstOrDefault();
            tableEntity.DurationMonth = subscriptionTypeEntity.DurationMonth;
            tableEntity.Description = subscriptionTypeEntity.Description;
            tableEntity.Price = subscriptionTypeEntity.Price;
            tableEntity.Name = subscriptionTypeEntity.Name;
            tableEntity.Id = subscriptionTypeEntity.Id;
            tableEntity.InvoiceSubscriptionTypeJson = subscriptionTypeEntity.InvoicesJson;
            tableEntity.InvoicesJson = subscriptionTypeEntity.InvoicesJson;
            tableClient.UpdateEntity(tableEntity, tableEntity.ETag);
        }
    }
}
