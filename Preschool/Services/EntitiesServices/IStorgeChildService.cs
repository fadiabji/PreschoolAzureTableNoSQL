using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public interface IStorgeChildService
    {
        void AddChildToTable(ChildEntity child);

        List<ChildEntity> GetChildEntities();

        public ChildEntity GetChildEntityById(int? id);

        void DeleteChildEntity(ChildEntity childEntity);
        void UpdateChildEntity(ChildEntity childEntity);

        bool IsChildExists(int id);

    }
}
