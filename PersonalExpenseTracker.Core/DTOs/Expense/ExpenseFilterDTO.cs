using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.DTOs.Expense
{
    public class ExpenseFilterDTO
    {
        public Guid UserId { get; set; }
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
        public Guid? CategoryId { get; set; }
        public double? Amount { get; set; }
        public string? AmountOperator { get; set; }
        public string? Description { get; set; }
    }
}
