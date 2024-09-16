using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.DTOs.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public double Salary { get; set; }
    }
}
