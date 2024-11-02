using BilgeCollege.MODELS.Abstracts;

namespace BilgeCollege.MODELS.Concretes
{
    public class Teacher : BaseEntity, I_UserInterface
    {
        public Teacher()
        {
            DaySchedules = new List<DaySchedule>
            {
                new DaySchedule { Day = "Monday" },
                new DaySchedule { Day = "Tuesday" },
                new DaySchedule { Day = "Wednesday" },
                new DaySchedule { Day = "Thursday" },
                new DaySchedule { Day = "Friday" }
            };
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TCK { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }

        // Relations
        public MainTopic? MainTopic { get; set; }
        public int? MainTopicId { get; set; }

        public List<AltTopic>? AltTopics { get; set; }   
        public List<DaySchedule>? DaySchedules { get; set; }
    }
}
