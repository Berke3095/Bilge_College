using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class Classroom : BaseEntity
    {
        public Classroom()
        {
            TotalStudents = 0;
        }

        public string? Grade { get; set; }
        public int MaxCapacity { get; set; } = 25;
        public int TotalStudents { get; set; }

        // Relations
        public List<Student>? Students { get; set; }
        public List<Classrooms_AltTopics>? Classrooms_AltTopics { get; set; }
    }
}
