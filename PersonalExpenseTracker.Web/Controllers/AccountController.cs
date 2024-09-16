using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Core.Domain.Entities;
using PersonalExpenseTracker.Core.DTOs.User;
using PersonalExpenseTracker.Core.ServiceContracts;
using PersonalExpenseTracker.Infrastructure.Identity;
using PersonalExpenseTracker.Web.Filters;
using PersonalExpenseTracker.Web.Models.ViewModel.Account;
using System.Text;

namespace PersonalExpenseTracker.Web.Controllers
{
   
    public class AccountController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
        }


        [HttpGet]
        [RedirectIfAuthenticatedAttribute]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [RedirectIfAuthenticatedAttribute]
        public async Task<IActionResult> Signup(SignupViewModel signupViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(signupViewModel);
            }
            // Call the User service to create the User
            var userCreateDto = new UserCreateDTO()
            {
                FullName = signupViewModel.FullName
            };
            var userDTO = await _userService.CreateUserAsync(userCreateDto);

            // now create the account
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserId = userDTO.Id,
                Email = signupViewModel.Email,
                UserName = signupViewModel.Email,
                NormalizedEmail = signupViewModel.Email.ToUpper()
            };

            var identityResult = await _userManager.CreateAsync(applicationUser, signupViewModel.Password);

            if (identityResult.Succeeded)
            {
                await _signInManager.SignInAsync(applicationUser, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(signupViewModel);
        }

        [HttpGet]
        [RedirectIfAuthenticatedAttribute]
        public IActionResult Signin()
        {
            return View();
        }


        [HttpPost]
        [RedirectIfAuthenticatedAttribute]
        public async Task<IActionResult> Signin(SigninViewModel signinViewModel)
        {

            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager
                    .PasswordSignInAsync(signinViewModel.Email, signinViewModel.Password, isPersistent: true, lockoutOnFailure: true);

                if (signInResult.Succeeded)
                {
                   // this line is not required right now
                    var applicationUser = _userManager.FindByEmailAsync(signinViewModel.Email);
                    return RedirectToAction("Index", "User");
                }

            }

            ViewBag.LoginFailed = "Invalid login attempt !!!";
            return View(signinViewModel);

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
