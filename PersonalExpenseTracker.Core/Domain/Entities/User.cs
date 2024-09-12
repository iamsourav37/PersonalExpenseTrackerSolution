using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        [MinLength(3, ErrorMessage ="Name must be atleast 3 character long !!!")]
        public string FullName { get; set; }
        public double? Salary { get; set; }


        // Expense
        public ICollection<Expense> Expenses { get; set; }

    }
}
