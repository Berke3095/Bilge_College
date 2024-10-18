using BilgeCollege.MODELS.Abstracts;
using BilgeCollege.MODELS.Enums;
using BilgeCollege.MODELS.Utils;

namespace BilgeCollege.MODELS.Concretes
{
    public class Student : BaseEntity
    {
        public Student()
        {
            SchoolNo = SchoolNoGenerator.GetSchoolNo();
        }

        private double _finalGrade;

        public string SchoolNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string Email { get; set; }

        public string FinishedSchool { get; set; } // Middle school name
        public double FinalGrade // Middle school final grade
        {
            get
            {
                return _finalGrade;
            }
            set
            {
                if(value <= 100 && value >= 0) _finalGrade = value;
                else if (value >= 100) _finalGrade = 100;
                else if (value <= 0) _finalGrade = 0;
            }
        } 

        // Relations
        public Guardian Guardian { get; set; }
        public int GuardianId { get; set; }

        public Classroom Classroom { get; set; }
        public int ClassroomId { get; set; }
    }
}
