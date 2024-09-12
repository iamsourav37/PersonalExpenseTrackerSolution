using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.Domain.Entities
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }

        // Expense  
        public ICollection<Expense> Expenses { get; set; }
    }
}
