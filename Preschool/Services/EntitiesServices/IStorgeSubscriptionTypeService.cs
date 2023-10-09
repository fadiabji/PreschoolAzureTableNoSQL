using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public interface IStorgeSubscriptionTypeService
    {
        void AddSubscriptionTypeToTable(SubscriptionTypeEntity subscriptionType);

        List<SubscriptionTypeEntity> GetSubscriptionTypeEntities();

        public SubscriptionTypeEntity GetSubscriptionTypeEntityById(int? id);

        void DeleteSubscriptionTypeEntity(SubscriptionTypeEntity subscriptionTypeEntity);
        void UpdateSubscriptionTypeEntity(SubscriptionTypeEntity subscriptionTypeEntity);

        bool IsSubscriptionTypeExists(int id);
    }
}
