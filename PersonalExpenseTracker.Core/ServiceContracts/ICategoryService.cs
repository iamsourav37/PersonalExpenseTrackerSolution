using PersonalExpenseTracker.Core.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.ServiceContracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoryAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(Guid categoryId);
    }
}
