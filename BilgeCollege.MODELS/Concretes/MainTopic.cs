using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class MainTopic : BaseEntity
    {
        public string TopicName { get; set; }

        // Relations
        public List<AltTopic>? AltTopics { get; set; }
    }
}
