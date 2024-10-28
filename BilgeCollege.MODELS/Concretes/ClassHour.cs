using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class ClassHour : BaseEntity
    {
        // Relations
        public AltTopic? AltTopic { get; set; }
        public int? AltTopicId { get; set; }

        public DaySchedule? DaySchedule { get; set; }
        public int? DayScheduleId { get; set; }
    }
}
