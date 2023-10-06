using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public interface IStorgeClassroomService
    {
        void AddClassroomToTable(ClassroomEntity classroomEntity);

        List<ClassroomEntity> GetClassroomEntities();

        public ClassroomEntity GetClassroomEntityById(int? id);

        void DeleteClassroomEntity(ClassroomEntity classroomEntity);
        void UpdateClassroomEntity(ClassroomEntity classroomEntity);

        bool IsClassroomExists(int id);

    }
}
