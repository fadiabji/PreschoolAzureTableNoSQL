using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;

namespace Preschool.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _db;

        public AttendanceService(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<IEnumerable<Attendance>> GetAttendances()
        {
            return await _db.Attendances.ToListAsync();
        }

        public async Task<Attendance> GetAttendanceById(int? id)
        {
            return await _db.Attendances.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void AddAttendance(Attendance attendance)
        {
            _db.Attendances.Add(attendance);
            _db.SaveChanges();
        }

        public void UpdateAttendance(Attendance attendance)
        {
            _db.Attendances.Update(attendance);
            _db.SaveChanges();
        }

        public void RemoveAttendance(Attendance attendance)
        {
            _db.Remove(attendance);
            _db.SaveChanges();
        }

        public bool IsExists(int? id)
        {
            return _db.Attendances.Any(e => e.Id == id);
        }
    }
}
