using Preschool.Models;

namespace Preschool.Services
{
    public interface IAssetsService
    {
        Task<IEnumerable<Asset>> GetAssets();

        Task<Asset> GetAssetById(int? id);

        void AddAsset(Asset asset);

        void UpdateAsset(Asset asset);

        public void RemoveAsset(Asset asset);

        bool IsExists(int? id);
    }
}
