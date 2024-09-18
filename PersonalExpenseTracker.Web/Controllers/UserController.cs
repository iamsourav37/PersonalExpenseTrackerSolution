using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Core.DTOs.User;
using PersonalExpenseTracker.Core.ServiceContracts;
using PersonalExpenseTracker.Infrastructure.Identity;
using PersonalExpenseTracker.Web.Models.ViewModel.User;
using System.Security.Claims;

namespace PersonalExpenseTracker.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            this._userService = userService;
            this._userManager = userManager;
        }

        private async Task<UserDTO> GetCurrentUser()
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user's ID
            var applicationUser = await _userManager.FindByIdAsync(identityUserId); // Load the ApplicationUser 
            var usreId = Guid.Parse(applicationUser.UserId.ToString());
            return await _userService.GetUserByIdAsync(usreId);
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await this.GetCurrentUser();

            ViewBag.FullName = currentUser.FullName;
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel userEditViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await GetCurrentUser();
                var userUpdateDto = new UserUpdateDTO()
                {
                    Id = user.Id,
                    FullName = userEditViewModel.FullName,
                    Salary = userEditViewModel.Salary
                };
               var result = await _userService.UpdateUserAsync(userUpdateDto);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
