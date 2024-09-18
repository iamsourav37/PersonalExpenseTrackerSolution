namespace PersonalExpenseTracker.Web.Models.ViewModel.Expense
{
    public class ExpenseViewModel
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
