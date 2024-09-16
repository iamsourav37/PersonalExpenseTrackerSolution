using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.DTOs.User
{
    public class UserCreateDTO
    {
        public string FullName { get; set; }
        public double Salary { get; set; }
    }
}
