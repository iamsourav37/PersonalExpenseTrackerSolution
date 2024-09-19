using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Web.Models.ViewModel.Expense
{
    public class ExpenseCreateViewModel
    {
        [DataType(DataType.Currency)]
        [Required]
        public double Amount { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Expense Date")]
        public DateTime ExpenseDate { get; set; } = DateTime.Now;
        public IEnumerable<SelectListItem>? CategoryList { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public Guid CategoryId { get; set; }
    }
}
