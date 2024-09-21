using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Core.ServiceContracts;
using PersonalExpenseTracker.Web.Models.ViewModel.Category;

namespace PersonalExpenseTracker.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var allCategories = await _categoryService.GetAllCategoryAsync();
            var categoryViewModel = allCategories.Select(catetoryDto => new CategoryViewModel()
            {
                Name = catetoryDto.Name,
            });
            return View(categoryViewModel);
        }
    }
}
