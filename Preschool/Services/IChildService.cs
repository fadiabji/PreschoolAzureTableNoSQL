using Preschool.Models;
using System.Runtime.CompilerServices;

namespace Preschool.Services
{
    public interface IChildService
    {
        Task<IEnumerable<Child>> GetChildren();
        Task<Child> GetChildById(int? id);

        Task<List<Child>> GetChildByClassroomId(int? id);

        void EnrollChild(Child child);

        void UpdateChildEnrollment(Child child);

        public void RemoveChild(Child child);


        bool IsExists(int? id);
    }
}
