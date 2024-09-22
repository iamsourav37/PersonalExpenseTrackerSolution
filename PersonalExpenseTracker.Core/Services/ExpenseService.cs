using PersonalExpenseTracker.Core.Domain.Entities;
using PersonalExpenseTracker.Core.Domain.RepositoryContracts;
using PersonalExpenseTracker.Core.DTOs.Expense;
using PersonalExpenseTracker.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ExpenseService(IExpenseRepository expenseRepository, ICategoryRepository categoryRepository)
        {
            this._expenseRepository = expenseRepository;
            this._categoryRepository = categoryRepository;
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
                    CategoryId = expense.CategoryId,
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

        public async Task<IEnumerable<ExpenseDTO>> GetExpensesByFilter(ExpenseFilterDTO expenseFilterDTO)
        {
            if (string.IsNullOrEmpty(expenseFilterDTO.UserId.ToString()))
            {
                return null;
            }


            #region Prepare the Expression
            Expression<Func<Expense, bool>> filterExpression = expense => expense.UserId == expenseFilterDTO.UserId;

            if (expenseFilterDTO.StartDate.HasValue)
            {
                filterExpression = CombineExpressions(filterExpression, expense => expense.ExpenseDate >= expenseFilterDTO.StartDate);
            }

            if (expenseFilterDTO.EndDate.HasValue)
            {
                filterExpression = CombineExpressions(filterExpression, expense => expense.ExpenseDate <= expenseFilterDTO.EndDate);
            }
            if (expenseFilterDTO.CategoryId.HasValue)
            {
                filterExpression = CombineExpressions(filterExpression, expense => expense.CategoryId == expenseFilterDTO.CategoryId);
            }
            if (expenseFilterDTO.Amount.HasValue)
            {
                filterExpression = CombineExpressions(filterExpression, GetAmountFilter(expenseFilterDTO.Amount ?? 0, expenseFilterDTO.AmountOperator));
            }
            if (!string.IsNullOrEmpty(expenseFilterDTO.Description))
            {
                filterExpression = CombineExpressions(filterExpression, expense => expense.Description.Contains(expenseFilterDTO.Description));
            }
            #endregion

            var expressionResult = await _expenseRepository.GetByExpressionAsync(filterExpression);

            // For Category Name
            var categoryList = await _categoryRepository.GetAllCategoryAsync();
            Dictionary<Guid, string> categoryDictionary = new Dictionary<Guid, string>();
            foreach (var category in categoryList)
            {
                categoryDictionary.Add(category.CategoryId, category.CategoryName);
            }


            var filterResultExpenseDtoList = expressionResult.Select(expense => new ExpenseDTO()
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Description = expense.Description,
                CategoryId = expense.CategoryId,
                ExpenseDate = expense.ExpenseDate,
                CategoryName = categoryDictionary.GetValueOrDefault(expense.CategoryId)
            });
            return filterResultExpenseDtoList;
        }

        private Expression<Func<Expense, bool>> CombineExpressions(Expression<Func<Expense, bool>> expr1, Expression<Func<Expense, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(Expense), "e");

            var combined = Expression.Lambda<Func<Expense, bool>>(
                Expression.AndAlso(
                    Expression.Invoke(expr1, parameter),
                    Expression.Invoke(expr2, parameter)
                ),
                parameter
            );

            return combined;
        }


        private Expression<Func<Expense, bool>> GetAmountFilter(double amount, string amountOperator)
        {
            switch (amountOperator)
            {
                case "GT":
                    return expense => expense.Amount > amount;
                case "GE":
                    return expense => expense.Amount >= amount;
                case "LT":
                    return expense => expense.Amount < amount;
                case "LE":
                    return expense => expense.Amount <= amount;
                case "EQ":
                    return expense => expense.Amount == amount;
                default:
                    return expense => expense.Amount == amount;

            }
        }
    }
}
