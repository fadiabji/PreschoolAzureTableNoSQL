using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;

namespace Preschool.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        public readonly ApplicationDbContext _db;

        public SubscriptionService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Subscription>> GetSubscriptions()
        {
            return await _db.Subscriptions.Include(s => s.SubscriptionType).Where(s => s.IsActive == true).OrderBy(s =>s.ExpireAt).ToListAsync();
        }

        public async Task<Subscription> GetSubscriptionById(int? id)
        {
            return await _db.Subscriptions.Include(s => s.SubscriptionType).SingleOrDefaultAsync(s => s.Id == id);
        }


        public void AddSubscription(Subscription subscriptionType)
        {
            _db.Subscriptions.Add(subscriptionType);
            _db.SaveChanges();
        }
        public void UpdateSubscriptionRegistration(Subscription subscription)
        {
            _db.Subscriptions.Update(subscription);
            _db.SaveChanges();
        }

        public void RemoveSubscription(Subscription subscription)
        {
            _db.Remove(subscription);
            _db.SaveChanges();
        }
        public bool IsExists(int? id)
        {
            return _db.Subscriptions.Any(s => s.Id == id) == true;
        }
    }
}
