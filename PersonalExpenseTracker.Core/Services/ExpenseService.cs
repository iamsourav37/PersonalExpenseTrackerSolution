using PersonalExpenseTracker.Core.Domain.Entities;
using PersonalExpenseTracker.Core.Domain.RepositoryContracts;
using PersonalExpenseTracker.Core.DTOs.Expense;
using PersonalExpenseTracker.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            this._expenseRepository = expenseRepository;
        }
        public async Task<ExpenseDTO> CreateExpenseAsync(ExpeneCreateDTO expeneCreateDTO)
        {
            var expense = new Expense()
            {
                Amount = expeneCreateDTO.Amount,
                Description = expeneCreateDTO.Description,
                CategoryId = expeneCreateDTO.CategoryId,
                UserId = expeneCreateDTO.UserId,
                ExpenseDate = expeneCreateDTO.ExpenseDate,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            expense = await _expenseRepository.CreateExpenseAsync(expense);
            var expenseDto = new ExpenseDTO()
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Description = expense.Description ?? string.Empty,
                ExpenseDate = expense.ExpenseDate,
                CategoryId = expense.CategoryId,
            };
            return expenseDto;
        }

        public async Task DeleteExpenseAsync(Guid expenseId)
        {
            await _expenseRepository.DeleteExpenseAsync(expenseId);
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllExpenseAsync(Guid userId)
        {
            var expenses = await _expenseRepository.GetAllExpenseAsync(userId);
            var expensesDto = expenses.Select(expense => new ExpenseDTO()
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Description = expense.Description ?? string.Empty,
                CategoryId = expense.CategoryId,
                ExpenseDate = expense.ExpenseDate
            }).ToList();

            return expensesDto;
        }

        public async Task<ExpenseDTO> UpdateExpenseAsync(ExpenseUpdateDTO expenseUpdateDTO)
        {
            var existingExpense = await _expenseRepository.GetExpenseByIdAsync(expenseUpdateDTO.Id);
            if (existingExpense == null)
            {
                return null;
            }

            existingExpense.Amount = expenseUpdateDTO.Amount;
            existingExpense.Description = expenseUpdateDTO.Description;
            existingExpense.ExpenseDate = expenseUpdateDTO.ExpenseDate;
            existingExpense.UpdatedAt = DateTime.Now;
            existingExpense.CategoryId = expenseUpdateDTO.CategoryId;
            existingExpense = await _expenseRepository.UpdateExpenseAsync(existingExpense);

            var expenseDto = new ExpenseDTO()
            {
                Id = existingExpense.Id,
                Amount = existingExpense.Amount,
                Description = existingExpense.Description,
                CategoryId = existingExpense.CategoryId,
                ExpenseDate = existingExpense.ExpenseDate
            };
            return expenseDto;
        }

        public async Task<ExpenseDTO> GetExpenseByIdAsync(Guid id)
        {
            try
            {
                var expense = await _expenseRepository.GetExpenseByIdAsync(id);
                return new ExpenseDTO()
                {
                    Amount = expense.Amount,
                    CategoryId= expense.CategoryId,
                    Description = expense.Description,
                    ExpenseDate = expense.ExpenseDate,
                    Id = expense.Id
                    
                };

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"exception occured in GetExpenseByIdAsync(), details: {ex.Message}");
            }
            return null;
        }
    }
}
