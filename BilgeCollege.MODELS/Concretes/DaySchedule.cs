using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class DaySchedule : BaseEntity
    {
        // Relations
        public Classroom Classroom { get; set; }
        public int ClassroomId { get; set; }

        public List<DaySchedule_AltTopic>? DaySchedule_AltTopics { get; set; }
    }
}
