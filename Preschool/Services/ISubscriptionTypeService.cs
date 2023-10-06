using Preschool.Models;

namespace Preschool.Services
{
    public interface ISubscriptionTypeService
    {
        Task<IEnumerable<SubscriptionType>> GetSubscriptionTypes();
        Task<SubscriptionType> GetSubscriptionTypeById(int? id);

        void AddSubscriptionType(SubscriptionType subscriptionType);

        void UpdateSubscriptionTypeRegistration(SubscriptionType subscriptionType);

        public void RemoveSubscriptionType(SubscriptionType subscriptionType);


        bool IsExists(int? id);
    }
}
