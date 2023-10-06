using Preschool.Models;

namespace Preschool.Services
{
    public interface IExpenseSevice
    {
        Task<IEnumerable<Expense>> GetExpenses();

        Task<Expense> GetExpenseById(int? id);

        void AddExpense(Expense expense);

        void UpdateExpense(Expense expense);

        void RemoveExpense(Expense expense);

        bool IsExists(int? id);



      
    }
}
