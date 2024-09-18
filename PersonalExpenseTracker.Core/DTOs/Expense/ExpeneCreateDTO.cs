using PersonalExpenseTracker.Core.Domain.Entities;


namespace PersonalExpenseTracker.Core.DTOs.Expense
{
    public class ExpeneCreateDTO
    {
        public double Amount { get; set; }
        public string? Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
