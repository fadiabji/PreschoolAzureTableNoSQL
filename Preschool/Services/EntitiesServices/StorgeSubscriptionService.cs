using Azure.Data.Tables;
using Preschool.Models;
using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public class StorgeSubscriptionService : IStorgeSubscriptionService
    {

        private readonly IConfiguration _configuration;
        private readonly TableServiceClient _tableServiceClient;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();
        public StorgeSubscriptionService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tableServiceClient = new TableServiceClient(_configuration["AzureWebJobStorge"]);
        }


        public async void AddSubscriptionToTable(SubscriptionEntity subscription)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionsTable");
            tableClient.CreateIfNotExists();
            subscription.Id = random.Next(0, 10000000);
            subscription.RowKey = subscription.Id.ToString();
            await tableClient.AddEntityAsync(subscription);
        }

        public void DeleteSubscriptionEntity(SubscriptionEntity subscriptionEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionsTable");
            tableClient.CreateIfNotExists();
            var tableEntity = tableClient.Query<SubscriptionEntity>(c => c.RowKey == subscriptionEntity.RowKey).FirstOrDefault();

            tableClient.DeleteEntity(tableEntity.PartitionKey, tableEntity.RowKey);
        }

        public List<SubscriptionEntity> GetSubscriptionEntities()
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionsTable");
            tableClient.CreateIfNotExists();
            var list = tableClient.Query<SubscriptionEntity>().ToList();
            return list;
        }

        public SubscriptionEntity GetSubscriptionEntityById(int? id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionsTable");
            tableClient.CreateIfNotExists();
            var teacherEntity = tableClient.Query<SubscriptionEntity>(c => c.RowKey == id.ToString()).FirstOrDefault();
            return teacherEntity;
        }

        public bool IsSubscriptionExists(int id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionsTable");
            return tableClient.Query<SubscriptionEntity>().Any(s => s.Id == id);

        }

        public void UpdateSubscriptionEntity(SubscriptionEntity subscriptionEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "SubscriptionsTable");
            tableClient.CreateIfNotExists();
            var tableEntity = tableClient.Query<SubscriptionEntity>(s => s.RowKey == subscriptionEntity.RowKey).FirstOrDefault();
            tableEntity.ExpireAt = subscriptionEntity.ExpireAt;
            tableEntity.CreatedAt = subscriptionEntity.CreatedAt;
            tableEntity.IsActive = subscriptionEntity.IsActive;
            tableEntity.ChildId = subscriptionEntity.ChildId;
            tableEntity.ChildJson = subscriptionEntity.ChildJson;
            tableEntity.Id = subscriptionEntity.Id;
            tableEntity.SubscriptionTypeId = subscriptionEntity.SubscriptionTypeId;
            tableEntity.SubscriptionTypeJson = subscriptionEntity.SubscriptionTypeJson;
            tableClient.UpdateEntity(tableEntity, tableEntity.ETag);
        }
    }
}
