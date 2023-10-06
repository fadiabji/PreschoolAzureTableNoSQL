using Preschool.Models;

namespace Preschool.Services
{
    public interface ISubscriptionService
    {

        Task<IEnumerable<Subscription>> GetSubscriptions();
        Task<Subscription> GetSubscriptionById(int? id);

        void AddSubscription(Subscription subscriptionType);

        void UpdateSubscriptionRegistration(Subscription subscriptionType);

        public void RemoveSubscription(Subscription subscriptionType);


        bool IsExists(int? id);
    }
}
