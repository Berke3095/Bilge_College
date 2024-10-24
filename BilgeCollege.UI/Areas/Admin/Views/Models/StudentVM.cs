using BilgeCollege.MODELS.Enums;
using System.ComponentModel.DataAnnotations;

namespace BilgeCollege.UI.Areas.Admin.Views.Models
{
    public class StudentVM
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

        [Required(ErrorMessage = "You must enter the finished school of the student.")]
        public string FinishedSchool { get; set; }

        [Required(ErrorMessage = "You must enter the final grade of the student.")]
        [Range(0, 100, ErrorMessage = "Final grade must be between 0 and 100.")]
        public double FinalGrade { get; set; }

        [Required(ErrorMessage = "You must choose a gender for the student")]
        [Range(1, 2, ErrorMessage = "You must choose a gender for the student")]
        public GenderEnum Gender { get; set; }

        public int? GuardianId { get; set; }
    }
}
