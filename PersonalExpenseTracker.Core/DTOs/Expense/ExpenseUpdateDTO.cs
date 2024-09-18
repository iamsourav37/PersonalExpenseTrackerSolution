using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.DTOs.Expense
{
    public class ExpenseUpdateDTO
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public string? Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}
