using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Core.Domain.Entities;
using PersonalExpenseTracker.Core.Domain.RepositoryContracts;
using PersonalExpenseTracker.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Infrastructure.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ExpenseRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();
            return expense;
        }

        public async Task DeleteExpenseAsync(Guid? expenseId)
        {
            var expense = _dbContext.Expenses.FirstOrDefault(expense => expense.Id == expenseId);
            if (expense != null)
            {
                _dbContext.Expenses.Remove(expense);
                await _dbContext.SaveChangesAsync();
            }
            return;
        }

        public async Task<IEnumerable<Expense>> GetAllExpenseAsync(Guid? userId)
        {
            //return await _dbContext.Expenses.Include("User").ToListAsync();
            //return await _dbContext.Expenses.Include("User").Where(e => e.UserId == userId).ToListAsync();
            return await _dbContext.Expenses.Include(e => e.User).Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(Guid? expenseId)
        {
            return await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == expenseId);
        }

        public async Task<Expense> UpdateExpenseAsync(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
            await _dbContext.SaveChangesAsync();
            return expense;
        }
    }
}
