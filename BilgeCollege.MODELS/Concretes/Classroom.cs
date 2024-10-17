using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class Classroom : BaseEntity
    {
        public string ClassroomCode { get; set; }
        public int MaxCapacity { get; set; } = 25;

        // Relations
        public List<Student> Students { get; set; }
        public List<Classrooms_AltTopics> Classrooms_AltTopics { get; set; }
    }
}
