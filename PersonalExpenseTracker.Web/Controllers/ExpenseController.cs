using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalExpenseTracker.Core.Domain.Entities;
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
            var allExpenses = await _expenseService.GetAllExpenseAsync((Guid)userId);
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

                ViewBag.ErrorMessage = string.Join(" | ", errorMessages);
                return View(expenseCreateViewModel);
            }

            var expenseCreateDto = new ExpeneCreateDTO()
            {
                Amount = expenseCreateViewModel.Amount,
                CategoryId = expenseCreateViewModel.CategoryId,
                Description = expenseCreateViewModel.Description,
                ExpenseDate = expenseCreateViewModel.ExpenseDate,
                UserId = (Guid)await _userHelper.GetCurrentUser(User)
            };
            var result = await _expenseService.CreateExpenseAsync(expenseCreateDto);
            return RedirectToAction(nameof(ExpenseController.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid expenseId)
        {
            var expenseDto = await _expenseService.GetExpenseByIdAsync(expenseId);

            var categoryDtoList = await _categoryService.GetAllCategoryAsync();

            ExpenseUpdateViewModel expenseUpdateViewModel = new ExpenseUpdateViewModel()
            {
                Id = expenseId,
                Amount = expenseDto.Amount,
                CategoryId = expenseDto.CategoryId,
                Description = expenseDto.Description,
                ExpenseDate = expenseDto.ExpenseDate,
                CategoryList = categoryDtoList.Select<CategoryDTO, SelectListItem>(categoryDto =>
                    new SelectListItem { Value = categoryDto.Id.ToString(), Text = categoryDto.Name, Selected = expenseDto.CategoryId == expenseId ? true : false }
                )
            };

            return View(expenseUpdateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ExpenseUpdateViewModel expenseUpdateViewModel)
        {

            if (!ModelState.IsValid)
            {
                var categoryDtoList = await _categoryService.GetAllCategoryAsync();
                expenseUpdateViewModel.CategoryList = categoryDtoList.Select<CategoryDTO, SelectListItem>(categoryDto =>
                        new SelectListItem { Value = categoryDto.Id.ToString(), Text = categoryDto.Name, Selected = categoryDto.Id == expenseUpdateViewModel.CategoryId ? true : false }
                    );
                var errorMessages = ModelState.Values
                                  .SelectMany(v => v.Errors)
                                  .Select(e => e.ErrorMessage);

                ViewBag.ErrorMessage = string.Join(" | ", errorMessages);
                return View(expenseUpdateViewModel);
            }

            var expenseUpdateDto = new ExpenseUpdateDTO()
            {
                Id = expenseUpdateViewModel.Id,
                Amount = expenseUpdateViewModel.Amount,
                CategoryId = expenseUpdateViewModel.CategoryId,
                Description = expenseUpdateViewModel.Description,
                ExpenseDate = expenseUpdateViewModel.ExpenseDate
            };

            var result = await _expenseService.UpdateExpenseAsync(expenseUpdateDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid expenseId)
        {
            await _expenseService.DeleteExpenseAsync(expenseId);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> AdvancedFilter(ExpenseFilterViewModel expenseFilterViewModel)
        {

            if (expenseFilterViewModel != null && expenseFilterViewModel.Filter != null)
            {
                var expenseFilterDto = new ExpenseFilterDTO()
                {
                    Amount = expenseFilterViewModel.Filter.Amount,
                    AmountOperator = expenseFilterViewModel.Filter.Operator,
                    CategoryId = expenseFilterViewModel.Filter.CategoryId,
                    Description = expenseFilterViewModel.Filter.Description,
                    StartDate = expenseFilterViewModel.Filter.StartDate,
                    EndDate = expenseFilterViewModel.Filter.EndDate,
                    UserId = await _userHelper.GetCurrentUser(User) ?? Guid.Empty
                };
                var filterResult = await _expenseService.GetExpensesByFilter(expenseFilterDto);
                if (filterResult != null && filterResult.Count() > 0)
                {
                    var expenseViewModelList = filterResult.Select(expenseDto => new ExpenseViewModel()
                    {
                        Id = expenseDto.Id,
                        Amount = expenseDto.Amount,
                        CategoryId = expenseDto.CategoryId, 
                        CategoryName = expenseDto.CategoryName,
                        Description = expenseDto.Description,
                        ExpenseDate = expenseDto.ExpenseDate
                    });
                    expenseFilterViewModel.FilterResult = expenseViewModelList;
                }
            }

            var categoryList = await _categoryService.GetAllCategoryAsync();
            List<SelectListItem> categories = categoryList.Select(category => new SelectListItem() { Value = category.Id.ToString(), Text = category.Name }).ToList();
           ViewBag.OperatorList = Enum.GetValues(typeof(OperatorOption))
                        .Cast<OperatorOption>()
                        .Select(o => new SelectListItem
                        {
                            Value = o.ToString(),
                            Text = o.ToString(),
                            Selected = o.ToString().Equals("EQ") ? true : false
                        })
                        .ToList();


            ViewBag.CategoryList = categories;
            return View(expenseFilterViewModel);
        }

    }
}
