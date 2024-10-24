using System.ComponentModel.DataAnnotations;

namespace BilgeCollege.UI.Areas.Admin.Views.Models
{
    public class GuardianVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter the first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must enter the last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter the TCK.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TCK must be exactly 11 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "TCK must contain only numbers.")]
        public string TCK { get; set; }

        [Required(ErrorMessage = "You must enter the home address.")]
        public string HomeAddress { get; set; }
    }
}
