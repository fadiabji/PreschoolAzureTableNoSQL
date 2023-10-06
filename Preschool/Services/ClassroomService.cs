using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;
using Preschool.Models.ViewModels;

namespace Preschool.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly ApplicationDbContext _db;
        public ClassroomService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Classroom>> GetClasses()
        {
            return await _db.Classrooms.ToListAsync();
        }

        public async Task<Classroom> GetClassById(int? id)
        {
            return await _db.Classrooms.SingleOrDefaultAsync(c => c.Id == id);
        }

        public void CreateClass(Classroom cclass)
        {
            _db.Classrooms.Add(cclass);
            _db.SaveChanges();
        }

        public void RemoveClass(Classroom cclass)
        {
            _db.Remove(cclass);
            _db.SaveChanges();
        }

        public void UpdateClass(Classroom cclass)
        {
            _db.Classrooms.Update(cclass);
            _db.SaveChanges();
        }
        public bool IsExists(int? id)
        {
            return _db.Classrooms.Any(c => c.Id == id);
        }
    }
}
