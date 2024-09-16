using PersonalExpenseTracker.Core.Domain.Entities;
using PersonalExpenseTracker.Core.Domain.RepositoryContracts;
using PersonalExpenseTracker.Core.DTOs.User;
using PersonalExpenseTracker.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> CreateUserAsync(UserCreateDTO userCreateDTO)
        {
            var user = new User()
            {
                FullName = userCreateDTO.FullName,
                Salary = userCreateDTO.Salary,
            };
            await _userRepository.CreateUserAsync(user);
            var userDto = new UserDTO()
            {
                FullName = user.FullName,
                Id = user.UserId,
                Salary = user.Salary ?? 0.0
            };
            return userDto;
        }

        public Task DeleteUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> UpdateUserAsync(UserUpdateDTO userUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
