using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class DaySchedule : BaseEntity
    {
        public DaySchedule()
        {
            ClassHours = new List<ClassHour>
            {
                new ClassHour{DayScheduleId = Id},
                new ClassHour{DayScheduleId = Id},
                new ClassHour{DayScheduleId = Id},
                new ClassHour{DayScheduleId = Id},
                new ClassHour{DayScheduleId = Id},
                new ClassHour{DayScheduleId = Id},
                new ClassHour{DayScheduleId = Id},
                new ClassHour{DayScheduleId = Id}
            };
        }

        public string Day { get; set; }

        // Relations
        public Classroom? Classroom { get; set; }
        public int? ClassroomId { get; set; }

        public List<ClassHour> ClassHours { get; set; }
    }
}
