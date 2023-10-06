using Microsoft.EntityFrameworkCore.ChangeTracking;
using Preschool.Models;
using System.Threading.Tasks;

namespace Preschool.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<Attendance>> GetAttendances();

        Task<Attendance> GetAttendanceById(int? id);

        void AddAttendance(Attendance attendance);

        void UpdateAttendance(Attendance attendance);

        void RemoveAttendance(Attendance attendance);

        bool IsExists(int? id);

    }
}
