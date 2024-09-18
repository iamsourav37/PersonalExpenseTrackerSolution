
namespace PersonalExpenseTracker.Core.DTOs.Expense
{
    public class ExpenseDTO
    {
  
        public Guid Id { get; set; }
        
        public double Amount { get; set; }
        public string Description { get; set; }
        
        public DateTime ExpenseDate { get; set; } = DateTime.Now;

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }



    }
}
