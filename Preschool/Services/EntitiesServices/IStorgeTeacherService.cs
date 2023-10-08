using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public interface IStorgeTeacherService
    {

        void AddTeacherToTable(TeacherEntity teacher);

        List<TeacherEntity> GetTeacherEntities();

        public TeacherEntity GetTeacherEntityById(int? id);

        void DeleteTeacherEntity(TeacherEntity teacherEntity);
        void UpdateTeacherEntity(TeacherEntity teacherEntity);

        bool IsTeacherExists(int id);
    }
}
