using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Web.Models.ViewModel.Account
{
    public class SigninViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
