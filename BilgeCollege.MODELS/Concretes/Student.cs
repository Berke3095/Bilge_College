using BilgeCollege.MODELS.Abstracts;
using BilgeCollege.MODELS.Enums;

namespace BilgeCollege.MODELS.Concretes
{
    public class Student : BaseEntity, I_UserInterface
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TCK { get; set; }
        public string UserId { get; set; }

        public string SchoolNo { get; set; }
        public GenderEnum Gender { get; set; }

        public string FinishedSchool { get; set; } // Middle school name
        public double FinalGrade { get; set; } // Middle school final grade

        // Relations
        public Guardian? Guardian { get; set; }
        public int? GuardianId { get; set; }

        public Classroom? Classroom { get; set; }
        public int? ClassroomId { get; set; }
    }
}
