using Preschool.Models;
using System.Runtime.CompilerServices;

namespace Preschool.Services
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetTeachers();
        Task<Teacher> GetTeacherById(int? id);

        void RegistTeacher(Teacher teacher);

        void UpdateTeacherRegistration(Teacher teacher);

        public void RemoveTeacher(Teacher teacher);


        bool IsExists(int? id);

    }
}
