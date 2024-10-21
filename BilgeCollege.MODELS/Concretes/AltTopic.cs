using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class AltTopic : BaseEntity
    {
        public string TopicCode { get; set; }

        // Relations
        public MainTopic? MainTopic { get; set; }
        public int? MainTopicId { get; set; }

        public List<Classrooms_AltTopics>? Classrooms_AltTopics { get; set; }
    }
}
