using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Web.Models.ViewModel.Expense
{
    public class ExpenseCreateViewModel
    {
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
