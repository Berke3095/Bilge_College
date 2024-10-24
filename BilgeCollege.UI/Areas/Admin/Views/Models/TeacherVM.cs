using BilgeCollege.MODELS.Concretes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BilgeCollege.UI.Areas.Admin.Views.Models
{
    public class TeacherVM
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

        [Required(ErrorMessage = "You must enter the phone number.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone number must be exactly 11 digits.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must contain only numbers.")]
        public string PhoneNumber { get; set; }

        public int? MainTopicId { get; set; }
    }
}
