using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public interface IStorgeSubscriptionService
    {
        void AddSubscriptionToTable(SubscriptionEntity subscription);

        List<SubscriptionEntity> GetSubscriptionEntities();

        public SubscriptionEntity GetSubscriptionEntityById(int? id);

        void DeleteSubscriptionEntity(SubscriptionEntity subscriptionEntity);
        void UpdateSubscriptionEntity(SubscriptionEntity subscriptionEntity);

        bool IsSubscriptionExists(int id);
    }
}
