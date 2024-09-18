using PersonalExpenseTracker.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.Domain.RepositoryContracts
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpenseAsync(Guid? userId);
        Task<Expense> GetExpenseByIdAsync(Guid? expenseId);
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<Expense> UpdateExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(Guid? expenseId);
    }
}
