using PersonalExpenseTracker.Core.Domain.Entities;
using PersonalExpenseTracker.Core.Domain.RepositoryContracts;
using PersonalExpenseTracker.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public async Task CreateUserAsync(User user)
        {
            _applicationDbContext.Users.Add(user);
            await _applicationDbContext.SaveChangesAsync();
        }

        public Task DeleteUserByIdAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task GetUserByIdAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
