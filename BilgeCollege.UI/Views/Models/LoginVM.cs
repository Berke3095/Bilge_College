using System.ComponentModel.DataAnnotations;

namespace BilgeCollege.UI.Views.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "You need to enter an email")]
        [EmailAddress(ErrorMessage = "Enter something in email format!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You need to enter a password.")]
        public string Password { get; set; }
        public bool bRememberMe { get; set; }
    }
}
