using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PersonalExpenseTracker.Core.ServiceContracts;
using PersonalExpenseTracker.Infrastructure.Identity;
using PersonalExpenseTracker.Web.Helper;
using PersonalExpenseTracker.Web.Models.ViewModel.Expense;

namespace PersonalExpenseTracker.Web.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;
        private UserHelper _userHelper;

        public ExpenseController(IExpenseService expenseService, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this._expenseService = expenseService;
            _userHelper = new UserHelper(userManager, userService);
        }


        public async Task<IActionResult> Index()
        {
            var userId = await _userHelper.GetCurrentUser(User);
            var allExpenses = await _expenseService.GetAllExpenseAsync(userId);
            var expensesViewModel = allExpenses?.Select(e => new ExpenseViewModel()
            {
                Id = e.Id,
                Amount = e.Amount,
                Description = e.Description,
                CategoryId = e.CategoryId,
                ExpenseDate = e.ExpenseDate,    
            }).ToList();
            return View(expensesViewModel);
        }
    }
}
