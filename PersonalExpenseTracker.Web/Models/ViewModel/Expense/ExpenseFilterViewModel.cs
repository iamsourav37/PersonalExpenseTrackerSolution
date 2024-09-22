namespace PersonalExpenseTracker.Web.Models.ViewModel.Expense
{
    public class ExpenseFilterViewModel
    {
        public ExpenseAdvancedFilter Filter { get; set; }
        public IEnumerable<ExpenseViewModel> FilterResult { get; set; } = Enumerable.Empty<ExpenseViewModel>();

    }
}
