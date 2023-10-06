using Preschool.Models.Entities;

namespace Preschool.Services.EntitiesServices
{
    public interface IStorgeAttendanceService
    {
        void AddAttendanceToTable(AttendanceEntity attendanceEntity);

        List<AttendanceEntity> GetAttendanceEntities();

        public AttendanceEntity GetAttendanceEntityById(int? id);

        void DeleteAttendanceEntity(AttendanceEntity attendanceEntity);
        void UpdateAttendanceEntity(AttendanceEntity attendanceEntity);

        bool IsAttendanceExists(int id);
    }
}
