using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class Classroom : BaseEntity
    {
        public int MaxCapacity { get; set; } = 25;

        // Relations
        public List<Student> Students { get; set; }
        public List<MainTopic> MainTopics { get; set; }
    }
}
