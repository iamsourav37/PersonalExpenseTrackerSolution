using Microsoft.EntityFrameworkCore.Storage.Json;
using PersonalExpenseTracker.Core.Domain.RepositoryContracts;
using PersonalExpenseTracker.Core.DTOs.Category;
using PersonalExpenseTracker.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllCategoryAsync()
        {
            var categoryList = await _categoryRepository.GetAllCategoryAsync();

            return categoryList.Select(c => new CategoryDTO
            {
                Id = c.CategoryId,
                Name = c.CategoryName              

            }).ToList();
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            return new CategoryDTO { Id = category.CategoryId, Name=category.CategoryName };
        }
    }
}
