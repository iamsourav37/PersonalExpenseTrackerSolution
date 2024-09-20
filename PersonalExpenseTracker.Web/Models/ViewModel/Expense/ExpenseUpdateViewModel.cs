
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PersonalExpenseTracker.Web.Models.ViewModel.Expense
{
    public class ExpenseUpdateViewModel
    {
        public Guid Id { get; set; }
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
