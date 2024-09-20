using Microsoft.AspNetCore.Identity;
using PersonalExpenseTracker.Core.ServiceContracts;
using PersonalExpenseTracker.Infrastructure.Identity;
using System.Security.Claims;

namespace PersonalExpenseTracker.Web.Helper
{
    public class UserHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ClaimsPrincipal _claimsPrincipal;
        private readonly IUserService _userService;

        public UserHelper(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            this._userManager = userManager;
          
            this._userService = userService;
        }

        public async Task<Guid?> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
            var identityUserId = _claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user's ID
            var applicationUser = await _userManager.FindByIdAsync(identityUserId); // Load the ApplicationUser 
            return applicationUser?.UserId;
            

            //var userDto = await _userService.GetUserByIdAsync(usreId);
            //return userDto.Id;
        }
    }
}
