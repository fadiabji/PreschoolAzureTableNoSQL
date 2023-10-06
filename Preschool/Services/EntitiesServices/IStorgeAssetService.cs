using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public interface IStorgeAssetService
    {
        void AddAssetToTable(AssetEntity assetEntity);

        List<AssetEntity> GetAssetEntities();

        public AssetEntity GetAssetEntityById(int? id);

        void DeleteAssetEntity(AssetEntity assetEntity);
        void UpdateAssetEntity(AssetEntity assetEntity);

        bool IsAssetExists(int id);
    }
}
