using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class Classrooms_AltTopics
    {
        // Relations
        public AltTopic? AltTopic { get; set; }
        public int? AltTopicId { get; set; }

        public Classroom? Classroom { get; set; }
        public int? ClassroomId { get; set; }
    }
}
