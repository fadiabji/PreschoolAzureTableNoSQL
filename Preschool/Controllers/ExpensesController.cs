using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Preschool.Data;
using Preschool.Models;
using Preschool.Services;

namespace Preschool.Controllers
{
    
    public class ExpensesController : Controller
    {
        private readonly IExpenseSevice _expenseSevice;

        public ExpensesController(IExpenseSevice expenseSevice)
        {
            _expenseSevice = expenseSevice;
        }
        [Authorize(Roles = ("Admin"))]
        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            return View(await _expenseSevice.GetExpenses());
        }

        [Authorize(Roles = ("Admin"))]
        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var expense = await _expenseSevice.GetExpenseById(id);  
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }
        [Authorize(Roles = ("Admin,Teacher"))]
        // GET: Expenses/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = ("Admin,Teacher"))]
        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Expense expense)
        {
            if (ModelState.IsValid)
            {
                //expense.BuyerEmail = User.Identity.Name; 
                await Task.Run(() => _expenseSevice.AddExpense(expense));
                if (!User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(expense);
        }
        [Authorize(Roles = ("Admin"))]

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _expenseSevice.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }
        [Authorize(Roles = ("Admin"))]

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => _expenseSevice.UpdateExpense(expense));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }
        [Authorize(Roles = ("Admin"))]

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _expenseSevice.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);

        }


        [Authorize(Roles = ("Admin"))]
        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expenses = await _expenseSevice.GetExpenses();
            if (expenses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Expenses'  is null.");
            }
            var expense = expenses.FirstOrDefault(e => e.Id == id);
            if (expense != null)
            {
                _expenseSevice.RemoveExpense(expense);
            }

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = ("Admin"))]
        private bool ExpenseExists(int id)
        {
            return _expenseSevice.IsExists(id);
        }
    }
}
