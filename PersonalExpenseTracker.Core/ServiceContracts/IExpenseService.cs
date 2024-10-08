﻿using PersonalExpenseTracker.Core.DTOs.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.ServiceContracts
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDTO>> GetAllExpenseAsync(Guid userId);
        Task<IEnumerable<ExpenseDTO>> GetExpensesByFilter(ExpenseFilterDTO expenseFilterDTO);
        Task<ExpenseDTO> CreateExpenseAsync(ExpeneCreateDTO expeneCreateDTO);
        Task<ExpenseDTO> UpdateExpenseAsync(ExpenseUpdateDTO expenseUpdateDTO);
        Task DeleteExpenseAsync(Guid expenseId);
        Task<ExpenseDTO> GetExpenseByIdAsync(Guid id);
    }
}
