using Microsoft.AspNetCore.Identity;
using PersonalExpenseTracker.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid UserId { get; set; }
        public User  User { get; set; }
    }
}
