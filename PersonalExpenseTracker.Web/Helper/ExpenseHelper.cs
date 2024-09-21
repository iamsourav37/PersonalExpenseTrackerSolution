namespace PersonalExpenseTracker.Web.Helper
{
    public class ExpenseHelper
    {
        public static bool IsAmountNotZero(double amount)
        {
            return amount > 0;
        }
    }
}
