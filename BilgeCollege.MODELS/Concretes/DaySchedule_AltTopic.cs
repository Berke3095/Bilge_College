using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class DaySchedule_AltTopic : BaseEntity
    {
        // Relations
        public DaySchedule? DaySchedule { get; set; }
        public int? DayScheduleId { get; set; }

        public AltTopic? AltTopic { get; set; }
        public int? AltTopicId { get; set; }
    }
}
