using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Web.Models.ViewModel.Expense
{
    public class ExpenseAdvancedFilter
    {
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; } = DateTime.Now;
        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; } = DateTime.Now;
        public Guid? CategoryId { get; set; }
        public double? Amount { get; set; }
        //public IEnumerable<SelectListItem> AmountOperator { get; set; }
        public string Operator { get; set; }

        [DisplayName("Description Keyword")]
        public string? Description { get; set; }

    }

    public enum OperatorOption
    {
        EQ, GE, GT, LE, LT
    }
}
