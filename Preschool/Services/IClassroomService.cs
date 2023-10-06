using Preschool.Models;
using System.Runtime.CompilerServices;

namespace Preschool.Services
{
    public interface IClassroomService
    {
        Task<IEnumerable<Classroom>> GetClasses();

        Task<Classroom> GetClassById(int? id);

        void CreateClass(Classroom cclass );

        void UpdateClass(Classroom cclass);

        public void RemoveClass(Classroom cclass);

        bool IsExists(int? id);
    }
}
