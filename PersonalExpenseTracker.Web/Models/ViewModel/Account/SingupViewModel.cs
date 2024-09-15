using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Web.Models.ViewModel.Account
{
    public class SignupViewModel
    {
        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
