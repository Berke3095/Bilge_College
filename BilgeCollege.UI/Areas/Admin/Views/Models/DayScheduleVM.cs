using BilgeCollege.MODELS.Concretes;

namespace BilgeCollege.UI.Areas.Admin.Views.Models
{
    public class DayScheduleVM
    {
        public DayScheduleVM()
        {
            AltTopics = new AltTopic[8];
        }

        public int Id { get; set; }
        public AltTopic[] AltTopics { get; set; }
        public DaySchedule DaySchedule { get; set; }
    }
}
