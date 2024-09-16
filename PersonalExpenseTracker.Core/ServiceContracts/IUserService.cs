using PersonalExpenseTracker.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalExpenseTracker.Core.ServiceContracts
{
    public interface IUserService
    {
        Task<UserDTO> CreateUserAsync(UserCreateDTO userDTO);
        Task<UserDTO> UpdateUserAsync(UserUpdateDTO userUpdateDTO);
        Task DeleteUserAsync(Guid userId);
        Task<UserDTO> GetUserByIdAsync(Guid userId);

    }
}
