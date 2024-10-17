using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class AltTopic : BaseEntity
    {
        // Relations
        public MainTopic MainTopic { get; set; }
        public int MainTopicId { get; set; }

        public List<Classroom> Classrooms { get; set; }
    }
}
