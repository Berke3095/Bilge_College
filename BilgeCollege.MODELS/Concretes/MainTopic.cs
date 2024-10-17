using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class MainTopic : BaseEntity
    {
        // Relations
        public List<AltTopic> AltTopics { get; set; }
    }
}
