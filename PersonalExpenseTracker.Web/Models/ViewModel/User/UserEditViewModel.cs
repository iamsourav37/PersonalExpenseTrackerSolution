using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonalExpenseTracker.Web.Models.ViewModel.User
{
    public class UserEditViewModel
    {
        [DisplayName("Full Name")]
        [Required]
        public string FullName { get; set; }
        
        public double? Salary { get; set; }
    }
}
