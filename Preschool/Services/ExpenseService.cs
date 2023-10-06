using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Preschool.Data;
using Preschool.Models;

namespace Preschool.Services
{
    public class ExpenseService : IExpenseSevice
    {

        private readonly ApplicationDbContext _db;
        public ExpenseService(ApplicationDbContext db)
        {
                _db = db;
        }
        public void AddExpense(Expense expense)
        {
            
            _db.Expenses.Add(expense);
            _db.SaveChanges();
        }

        public async Task<Expense> GetExpenseById(int? id)
        {
            return await _db.Expenses.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            return await _db.Expenses.ToListAsync();
        }

        public bool IsExists(int? id)
        {
            return _db.Expenses.Any(a => a.Id == id);
        }

        public void RemoveExpense(Expense expense)
        {
            _db.Remove(expense);
            _db.SaveChanges();
        }

        public void UpdateExpense(Expense expense)
        {
            _db.Expenses.Update(expense);
            _db.SaveChanges();
        }
    }
}
