using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class DaySchedule : BaseEntity
    {
        public string Day {  get; set; }

        // Relations
        public Classroom? Classroom { get; set; }
        public int? ClassroomId { get; set; }

        public List<AltTopic>? AltTopics { get; set; }
    }
}
