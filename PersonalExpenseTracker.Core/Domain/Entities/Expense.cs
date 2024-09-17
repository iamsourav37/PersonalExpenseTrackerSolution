using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.Domain.Entities
{
    public class Expense
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public double Amount { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime ExpenseDate { get; set; } = DateTime.Now;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        // Category
        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(Category.CategoryId))]
        public Category Category { get; set; }


        // User
        public Guid UserId { get; set; }
        [ForeignKey(nameof(User.UserId))]
        public User User { get; set; }

    }
}
