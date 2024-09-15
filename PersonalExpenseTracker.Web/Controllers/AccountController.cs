using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Web.Models.ViewModel.Account;
using System.Text;

namespace PersonalExpenseTracker.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel signupViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(signupViewModel);
            }
            // Call the account service
            return View();
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Signin(SigninViewModel signinViewModel)
        {

            // Call the account service
            return View();
        }
    }
}
