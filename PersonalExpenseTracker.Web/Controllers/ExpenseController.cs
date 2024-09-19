using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalExpenseTracker.Core.DTOs.Category;
using PersonalExpenseTracker.Core.DTOs.Expense;
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
        private readonly ICategoryService _categoryService;
        private UserHelper _userHelper;

        public ExpenseController(IExpenseService expenseService, UserManager<ApplicationUser> userManager, IUserService userService, ICategoryService categoryService)
        {
            this._expenseService = expenseService;
            this._categoryService = categoryService;
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

        public async Task<IActionResult> Create()
        {
            var categoryDtoList = await _categoryService.GetAllCategoryAsync();
            var expenseCreateViewModel = new ExpenseCreateViewModel()
            {
                CategoryList = categoryDtoList.Select<CategoryDTO, SelectListItem>(categoryDto =>
                    new SelectListItem { Value = categoryDto.Id.ToString(), Text = categoryDto.Name }
                )
            };
            return View(expenseCreateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseCreateViewModel expenseCreateViewModel)
        {
            if (!ModelState.IsValid)
            {

                var categoryDtoList = await _categoryService.GetAllCategoryAsync();
                expenseCreateViewModel.CategoryList = categoryDtoList.Select<CategoryDTO, SelectListItem>(categoryDto =>
                        new SelectListItem { Value = categoryDto.Id.ToString(), Text = categoryDto.Name }
                    );
                var errorMessages = ModelState.Values
                                  .SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage);

                ViewBag.ErrorMessage = string.Join(" | ", errorMessages); ;
                return View(expenseCreateViewModel);
            }

            var expenseCreateDto = new ExpeneCreateDTO()
            {
                Amount = expenseCreateViewModel.Amount,
                CategoryId = expenseCreateViewModel.CategoryId,
                Description = expenseCreateViewModel.Description,
                ExpenseDate = expenseCreateViewModel.ExpenseDate,
                UserId = await _userHelper.GetCurrentUser(User)
            };
            var result = await _expenseService.CreateExpenseAsync(expenseCreateDto);
            return RedirectToAction(nameof(ExpenseController.Index));
        }
    }
}
