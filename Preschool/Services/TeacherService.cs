using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;
using Preschool.Models.ViewModels;

namespace Preschool.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _db;
        public TeacherService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Teacher>> GetTeachers()
        {
            return await _db.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetTeacherById(int? id)
        {
            return await _db.Teachers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void RegistTeacher(Teacher teacher)
        {
            _db.Teachers.Add(teacher);
            _db.SaveChanges();
        }

        public void UpdateTeacherRegistration(Teacher teacher)
        {
            _db.Teachers.Update(teacher);
            _db.SaveChanges();
        }

        public void RemoveTeacher(Teacher teacher)
        {
            _db.Remove(teacher);
            _db.SaveChanges();
        }

        public bool IsExists(int? id)
        {
            return _db.Teachers.Any(t => t.Id == id);
        }


    }
}
