using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;

namespace Preschool.Services
{
    public class SubscriptionTypeService : ISubscriptionTypeService
    {
        public readonly ApplicationDbContext _db;

        public SubscriptionTypeService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<SubscriptionType>> GetSubscriptionTypes()
        {
            return await _db.SubscriptionTypes.ToListAsync();
        }

        public async Task<SubscriptionType> GetSubscriptionTypeById(int? id)
        {
            return await _db.SubscriptionTypes.FindAsync(id);
        }


        public void AddSubscriptionType(SubscriptionType subscriptionType)
        {
            _db.SubscriptionTypes.Add(subscriptionType);
            _db.SaveChanges();
        }
        public void UpdateSubscriptionTypeRegistration(SubscriptionType subscriptionType)
        {
            _db.SubscriptionTypes.Update(subscriptionType);
            _db.SaveChanges();
        }

        public void RemoveSubscriptionType(SubscriptionType subscriptionType)
        {
            _db.Remove(subscriptionType);
            _db.SaveChanges();
        }
        public bool IsExists(int? id)
        {
            return _db.SubscriptionTypes.Any(s => s.Id == id) == true;
        }

    }
}
