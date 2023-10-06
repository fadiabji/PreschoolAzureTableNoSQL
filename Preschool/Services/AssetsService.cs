using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;

namespace Preschool.Services
{
    public class AssetsService : IAssetsService
    {

        private readonly ApplicationDbContext _db;

        public AssetsService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddAsset(Asset asset)
        {
            _db.Assets.Add(asset);
            _db.SaveChanges();
        }

        public async Task<Asset> GetAssetById(int? id)
        {
            return await _db.Assets.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Asset>> GetAssets()
        {
            return await _db.Assets.ToListAsync();
        }

        public bool IsExists(int? id)
        {
            return _db.Assets.Any(a => a.Id == id);
        }

        public void RemoveAsset(Asset asset)
        {
            _db.Remove(asset);
            _db.SaveChanges();
        }

        public void UpdateAsset(Asset asset)
        {
            _db.Assets.Update(asset);
            _db.SaveChanges();
        }
    }
}
