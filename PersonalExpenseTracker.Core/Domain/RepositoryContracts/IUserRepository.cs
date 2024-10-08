﻿using PersonalExpenseTracker.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.Domain.RepositoryContracts
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(Guid? id);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserByIdAsync(Guid? id);
    }
}
