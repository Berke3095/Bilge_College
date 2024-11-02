using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class DaySchedule : BaseEntity
    {
        public DaySchedule()
        {
            ClassHours = new List<ClassHour>
            {
                new ClassHour{DayScheduleId = Id, AltTopicId = 1}, // NONE alt topic
                new ClassHour{DayScheduleId = Id, AltTopicId = 1},
                new ClassHour{DayScheduleId = Id, AltTopicId = 1},
                new ClassHour{DayScheduleId = Id, AltTopicId = 1},
                new ClassHour{DayScheduleId = Id, AltTopicId = 1},
                new ClassHour{DayScheduleId = Id, AltTopicId = 1},
                new ClassHour{DayScheduleId = Id, AltTopicId = 1},
                new ClassHour{DayScheduleId = Id, AltTopicId = 1}
            };
        }

        public string Day { get; set; }

        // Relations
        public Classroom? Classroom { get; set; }
        public int? ClassroomId { get; set; }

        public Teacher? Teacher { get; set; }
        public int? TeacherId { get; set; }

        public List<ClassHour>? ClassHours { get; set; }
    }
}
