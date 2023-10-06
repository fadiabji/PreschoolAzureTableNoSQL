using Azure.Data.Tables;
using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public class StorgeAssetService : IStorgeAssetService
    {
        private readonly IConfiguration _configuration;
        private readonly TableServiceClient _tableServiceClient;
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();
        public StorgeAssetService(IConfiguration configuration)
        {
            _configuration = configuration;
            _tableServiceClient = new TableServiceClient(_configuration["AzureWebJobStorge"]);
        }

        public async void AddAssetToTable(AssetEntity asset)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AssetsTable");
            tableClient.CreateIfNotExists();
            asset.Id = random.Next(0, 10000000);
            asset.RowKey = asset.Id.ToString();
            await tableClient.AddEntityAsync(asset);

        }

        public List<AssetEntity> GetAssetEntities()
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AssetsTable");
            tableClient.CreateIfNotExists();
            var list = tableClient.Query<AssetEntity>().ToList();
            return list;
        }


        public AssetEntity GetAssetEntityById(int? id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AssetsTable");
            tableClient.CreateIfNotExists();
            var assetEntity = tableClient.Query<AssetEntity>().FirstOrDefault(c => c.Id == id);
            return assetEntity;
        }



        public void DeleteAssetEntity(AssetEntity assetEntity)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AssetsTable");
            tableClient.CreateIfNotExists();
            tableClient.DeleteEntity(assetEntity.PartitionKey, assetEntity.RowKey);

        }

        public void UpdateAssetEntity(AssetEntity assetEntitytoUpdate)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AssetsTable");
            tableClient.CreateIfNotExists();
            var assetEntity = tableClient.Query<AssetEntity>(a =>a.RowKey == assetEntitytoUpdate.RowKey).FirstOrDefault();
            assetEntitytoUpdate.ETag = assetEntity.ETag;
            tableClient.UpdateEntity(assetEntitytoUpdate, assetEntitytoUpdate.ETag);
        }


        public bool IsAssetExists(int id)
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(tableName: "AssetsTable");
            tableClient.CreateIfNotExists();
            return tableClient.Query<AssetEntity>().Any(c => c.Id == id);
        }
    }
}
