using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Preschool.Data;
using Preschool.Models;
using System.Runtime.CompilerServices;

namespace Preschool.Services
{
    public class ChildService : IChildService
    {
        private readonly ApplicationDbContext _db;
       
        public ChildService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Child>> GetChildren()
        {
            return await _db.Childern.ToListAsync();
        }

        public async Task<Child> GetChildById(int? id)
        {
            return await _db.Childern.Include(c => c.DocumentsImage).Include(c => c.Subscriptions).FirstOrDefaultAsync(c => c.Id == id);
            //return await _db.Childern.Include(c => c.DocumentsImage).FirstOrDefaultAsync(x => x.Id == id);
        }


        public  async Task<List<Child>> GetChildByClassroomId(int? id)
        {
            return  await Task.Run(()=> _db.Childern.Where(c => c.ClassroomId == id).ToList());
        }
        public void EnrollChild(Child child)
        {
            _db.Childern.Add(child);
            _db.SaveChanges();
        }

        public void UpdateChildEnrollment(Child child)
        {
            _db.Childern.Update(child);
            _db.SaveChanges();
        }

        public void RemoveChild(Child child)
        {
            _db.Remove(child);
            _db.SaveChanges();
        }

        public bool IsExists(int? id)
        {
            return _db.Childern.Any(e => e.Id == id);
        }
    }
}
