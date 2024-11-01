using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class Grade : BaseEntity
    {
        public double MidTermGrade { get; set; }
        public double FinalGrade { get; set; }
        public double PerformanceGrade { get; set; }
        public double Score
        {
            get
            {
                return ((MidTermGrade * 0.40) + (FinalGrade * 0.60) + PerformanceGrade) / 2;
            }
        }

        // Relations
        public AltTopic? AltTopic { get; set; }
        public int? AltTopicId { get; set; }

        public Student? Student { get; set; }
        public int? StudentId { get; set; }
    }
}
