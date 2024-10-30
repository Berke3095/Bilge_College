using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class Grade : BaseEntity
    {
        public double MidTermGrade { get; set; }
        public double FinalGrade { get; set; }

        // Relations
        public AltTopic? AltTopic { get; set; }
        public int? AltTopicId { get; set; }

        public Student? Student { get; set; }
        public int? StudentId { get; set; }
    }
}
